package valkov.vladimir.wifilogger;

import android.Manifest;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.ContextCompat;
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
        Button optionsButton = findViewById(R.id.button_options);
        Button loggedDataButton = findViewById(R.id.button_view_data);

        sendButton.setEnabled(false);


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
                            sendButton.setEnabled(true);
                        }
                        else
                        {
                            serviceButton.setText("Start Service");
                            sendButton.setEnabled(false);
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

        optionsButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, OptionsActivity.class);
                startActivity(intent);
            }
        });

        loggedDataButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, LoggedDataActivity.class);
                startActivity(intent);
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