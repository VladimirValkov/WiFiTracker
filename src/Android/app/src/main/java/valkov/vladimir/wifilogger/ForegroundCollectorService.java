package valkov.vladimir.wifilogger;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.IBinder;
import android.util.Log;

import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.concurrent.Executors;

import valkov.vladimir.wifilogger.db.LogData;
import valkov.vladimir.wifilogger.db.MainDB;

public class ForegroundCollectorService extends Service {

    WifiManager wifiManager = null;
    MainDB db = null;
    boolean collectorStarted = false;
    BroadcastReceiver mainReceiver = null;

    private void initializeCollector() {

        wifiManager = (WifiManager) this.getApplicationContext().getSystemService(Context.WIFI_SERVICE);
        db = MainDB.getInstance(this);

         mainReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context c, Intent intent) {
                String action = intent.getAction();

                switch (action){
                    case WifiManager.SCAN_RESULTS_AVAILABLE_ACTION:
                        boolean success = intent.getBooleanExtra(
                                WifiManager.EXTRA_RESULTS_UPDATED, false);
                        if (success) {
                            //scanSuccess();
                            List<ScanResult> results = wifiManager.getScanResults();
                            Date timestamp = Calendar.getInstance().getTime();
                            for (ScanResult res: results) {
                                LogData point = new LogData();
                                point.bssid = res.BSSID;
                                point.frequency = res.frequency;
                                point.signalLevel = res.level;
                                point.logTimeStamp = timestamp;
                                point.name = res.SSID;

                                db.logDao().insertPoint(point);
                            }
                        } else {
                            // scan failure handling
                            Log.d("vlad", "error2");
                        }
                        break;
                    case Globals.COMMAND_SERVICE_RUNNING:
                        reportState();
                        break;
                    case Globals.COMMAND_SERVICE_FORCE_SEND_DATA:
                        sendToServer();
                        break;

                }

            }
        };

        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION);
        intentFilter.addAction(Globals.COMMAND_SERVICE_RUNNING);
        intentFilter.addAction(Globals.COMMAND_SERVICE_FORCE_SEND_DATA);
        this.getApplicationContext().registerReceiver(mainReceiver, intentFilter);
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        if (wifiManager == null)
        {
            initializeCollector();
        }
        createNotificationChannel();
        if (!collectorStarted)
        {
            collectorStarted = true;
            Executors.newSingleThreadExecutor().execute(new Runnable() {
                @Override
                public void run() {
                    int flushTimeOut = 0;
                    while(collectorStarted)
                    {
                        try {
                            Thread.sleep(5000);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                        flushTimeOut++;
                        if (wifiManager != null)
                        {
                            wifiManager.startScan();
                        }
                        if (flushTimeOut >= 12){
                            flushTimeOut = 0;
                            sendToServer();
                        }
                    }
                }
            });
        }

        reportState();
        return super.onStartCommand(intent, flags, startId);
    }

    private void sendToServer() {
        Log.d("vlad", "sendToServer: " + db.logDao().getTimestamps().size());
        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        boolean state = true;
        String error = "";
        Intent answIntent = new Intent(Globals.COMMAND_SERVICE_FORCE_SEND_DATA_ANSWER);
        answIntent.putExtra(Globals.PARAMETER_SEND_STATE, state);
        answIntent.putExtra(Globals.PARAMETER_SEND_ERROR, error);
        sendBroadcast(answIntent);
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onDestroy() {
        wifiManager = null;
        db = null;
        collectorStarted = false;
        if(mainReceiver != null) this.getApplicationContext().unregisterReceiver(mainReceiver);
        reportState();
        stopForeground(true);
        stopSelf();
        super.onDestroy();
    }

    private void  createNotificationChannel() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel channel = new NotificationChannel("ForegroundCollectorService", "Foreground notification", NotificationManager.IMPORTANCE_DEFAULT);
            NotificationManager manager = getSystemService(NotificationManager.class);
            manager.createNotificationChannel(channel);
            Notification notification = new Notification.Builder(this, channel.getId())
                    .setContentTitle("WiFi Logger is running")
                    .setContentText("Collecting data ...")
                    .setSmallIcon(R.drawable.ic_launcher_foreground)
                    .build();
            //.setContentIntent(pendingIntent)
                    // .setSmallIcon(R.drawable.ic_screen_share_24)
            //.setLargeIcon(IconCompat.createWithResource(this,R.drawable.ic_screen_share_24).toIcon(this))

            startForeground(123,notification);
        }
    }

    private void reportState(){
        Intent answIntent = new Intent(Globals.COMMAND_SERVICE_RUNNING_ANSWER);
        answIntent.putExtra(Globals.PARAMETER_SERVICE_STATE, collectorStarted);
        ForegroundCollectorService.this.sendBroadcast(answIntent);
    }
}