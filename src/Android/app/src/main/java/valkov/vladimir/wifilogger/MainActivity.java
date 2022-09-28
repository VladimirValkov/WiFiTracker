package valkov.vladimir.wifilogger;

import android.Manifest;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.time.Instant;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import androidx.room.Room;
import valkov.vladimir.wifilogger.db.LogData;
import valkov.vladimir.wifilogger.db.MainDB;
import valkov.vladimir.wifilogger.db.TimestampConverter;

public class MainActivity extends AppCompatActivity {

    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    protected void onCreate(Bundle savedInstanceState) {


        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        MainDB db = MainDB.getInstance(this);

        TextView text = findViewById(R.id.textView);
        Button button = findViewById(R.id.button);
        Button button2 = findViewById(R.id.button2);
        Button button3 = findViewById(R.id.button3);

        WifiManager wifiManager = (WifiManager) this.getApplicationContext().getSystemService(Context.WIFI_SERVICE);

        BroadcastReceiver wifiScanReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context c, Intent intent) {
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
            }
        };

        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION);
        this.getApplicationContext().registerReceiver(wifiScanReceiver, intentFilter);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                boolean success = wifiManager.startScan();
                if (!success) {
                    // scan failure handling
                    //scanFailure();
                    Log.d("vlad", "error");
                }
            }
        });

        button2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                List<Date> ts = db.logDao().getTimestamps();

                for(Date d: ts){
                    List<LogData> pts = db.logDao().getPointsOfTimestamps(d);
                    int a = 0;
                }
            }
        });

        button3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent service = new Intent(MainActivity.this, ForegroundCollectorService.class);
                ContextCompat.startForegroundService(MainActivity.this, service);
            }
        });

        if (ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED ||
                ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED)
        {
            requestPermissions(new String[] { Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION }, 9999);
        }

    }

}