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

import java.util.Calendar;
import java.util.Date;
import java.util.List;

import valkov.vladimir.wifilogger.db.LogData;
import valkov.vladimir.wifilogger.db.MainDB;

public class MainActivity extends AppCompatActivity {
    BroadcastReceiver mainReceiver = null;

    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    protected void onCreate(Bundle savedInstanceState) {


        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        MainDB db = MainDB.getInstance(this);

        Button serviceButton = findViewById(R.id.button_service_control);
        Button sendButton = findViewById(R.id.button_send);
        Button button2 = findViewById(R.id.button_options);


         mainReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                switch (intent.getAction()){
                    case Globals.COMMAND_SERVICE_RUNNING_ANSWER:
                        boolean isRunning = intent.getBooleanExtra(Globals.PARAMETER_SERVICE_STATE, false);
                        serviceButton.setTag(isRunning);
                        if (isRunning)
                        {
                            serviceButton.setText("Stop Service");
                        }
                        else
                        {
                            serviceButton.setText("Start Service");
                        }
                        break;
                    case Globals.COMMAND_SERVICE_FORCE_SEND_DATA_ANSWER:
                        boolean send = intent.getBooleanExtra(Globals.PARAMETER_SEND_STATE, false);
                        String error = intent.getStringExtra(Globals.PARAMETER_SEND_ERROR);
                        CharSequence text = "";
                        if (send)
                        {
                            text = "Data has been sent.";

                        }
                        else{
                            text = "There was an error while sending.\n" + error;
                        }

                        Toast.makeText(getApplicationContext(), text, Toast.LENGTH_SHORT).show();
                        break;
                }
            }
        };
        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(Globals.COMMAND_SERVICE_RUNNING_ANSWER);
        intentFilter.addAction(Globals.COMMAND_SERVICE_FORCE_SEND_DATA_ANSWER);
        this.getApplicationContext().registerReceiver(mainReceiver, intentFilter);
        sendBroadcast(new Intent(Globals.COMMAND_SERVICE_RUNNING));



        serviceButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent service = new Intent(MainActivity.this, ForegroundCollectorService.class);
                if (serviceButton.getTag() != null)
                {
                    boolean running = (boolean) serviceButton.getTag();
                    if (running)
                    {
                        stopService(service);
                        return;
                    }
                }
                ContextCompat.startForegroundService(MainActivity.this, service);


            }
        });

        sendButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sendBroadcast(new Intent(Globals.COMMAND_SERVICE_FORCE_SEND_DATA));
            }
        });

        if (ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED ||
                ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED)
        {
            requestPermissions(new String[] { Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION }, 9999);
        }

    }

    @Override
    protected void onDestroy() {
       if (mainReceiver != null) this.getApplicationContext().unregisterReceiver(mainReceiver);
        super.onDestroy();
    }
}