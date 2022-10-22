package valkov.vladimir.wifilogger;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.google.gson.Gson;

import java.io.IOException;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import valkov.vladimir.wifilogger.db.MainDB;
import valkov.vladimir.wifilogger.db.Options;
import valkov.vladimir.wifilogger.db.OptionsDao;
import valkov.vladimir.wifilogger.models.ApiError;

public class OptionsActivity extends AppCompatActivity {
    EditText urlBox;
    EditText terminalIdBox;
    Options options = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_options);

        MainDB db = MainDB.getInstance(this);

        Button saveButton = findViewById(R.id.save_button);
        Button testButton = findViewById(R.id.test_button);
        urlBox = findViewById(R.id.url_edittext);
        terminalIdBox = findViewById(R.id.terminal_id_edittext);

        options = db.optionsDao().getOptions();
        if (options == null)
        {
            db.optionsDao().insertOptions(new Options());
            options = db.optionsDao().getOptions();
        }
        urlBox.setText(options.serverURL);
        terminalIdBox.setText(options.identifier);

        saveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                options.serverURL = urlBox.getText().toString();
                options.identifier = terminalIdBox.getText().toString();
                db.optionsDao().updateOptions(options);
                Toast.makeText(OptionsActivity.this,"Options updated", Toast.LENGTH_SHORT).show();
            }
        });

        testButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                OkHttpClient http = new OkHttpClient();

                try {
                    String url = urlBox.getText().toString();
                    if (url.endsWith("/")){
                        url = url.substring(0,url.length() - 1);
                    }
                    Request request = new Request.Builder()
                            .url(url + "/Api/test?id=" + terminalIdBox.getText())
                            .get()
                            .build();


                    http.newCall(request).enqueue(new Callback() {
                        @Override
                        public void onFailure(@NonNull Call call, @NonNull IOException e) {
                            showMessage("Error:" + e.getMessage());
                        }

                        @Override
                        public void onResponse(@NonNull Call call, @NonNull Response response) throws IOException {
                            try {
                                if (response.isSuccessful()) {
                                    showMessage("Connection is successful.");
                                } else {
                                    String body = response.body().string();
                                    ApiError error = new Gson().fromJson(body, ApiError.class);
                                    if (error != null)
                                    {
                                        showMessage("Error: " + error.getMessage());
                                    }
                                    else{
                                        showMessage("Error: " + response.message());
                                    }
                                }
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                } catch (Exception e) {
                    showMessage("Error: " + e.getMessage());
                }
            }
        });
    }

    void showMessage(String message)
    {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(OptionsActivity.this, message, Toast.LENGTH_LONG).show();
            }
        });
    }
}