package valkov.vladimir.wifilogger;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import valkov.vladimir.wifilogger.db.MainDB;
import valkov.vladimir.wifilogger.db.Options;
import valkov.vladimir.wifilogger.db.OptionsDao;

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
    }
}