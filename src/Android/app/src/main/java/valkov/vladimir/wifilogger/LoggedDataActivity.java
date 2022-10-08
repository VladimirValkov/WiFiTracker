package valkov.vladimir.wifilogger;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import valkov.vladimir.wifilogger.db.LogData;
import valkov.vladimir.wifilogger.db.MainDB;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.ExpandableListView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class LoggedDataActivity extends AppCompatActivity {

    ExpandableListView listView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_logged_data);

        listView = findViewById(R.id.group_list_view);
        LoadData();

    }

    void LoadData()
    {
        MainDB db = MainDB.getInstance(this);
        LogDataAdapter adapter = new LogDataAdapter(this, db);
        listView.setAdapter(adapter);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.view_data_menu, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        if (item.getItemId() == R.id.menu_refresh){
            LoadData();
        }
        return super.onOptionsItemSelected(item);
    }

    class LogDataAdapter extends BaseExpandableListAdapter {
        Context ctx;
        List<Date> groups;
        MainDB db;
        LayoutInflater inflater;
        LogDataAdapter(Context _ctx, MainDB _db){
            ctx = _ctx;
            db = _db;
            groups = db.logDao().getTimestamps();
            if (groups == null){
                groups = new ArrayList<>();
            }
            inflater = (LayoutInflater) ctx.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        }
        @Override
        public int getGroupCount() {
            return groups.size();
        }

        @Override
        public int getChildrenCount(int i) {
           return db.logDao().getPointsOfTimestamps(groups.get(i)).size();
        }

        @Override
        public Object getGroup(int i) {
            return null;
        }

        @Override
        public Object getChild(int i, int i1) {
            return null;
        }

        @Override
        public long getGroupId(int i) {
            return 0;
        }

        @Override
        public long getChildId(int i, int i1) {
            return 0;
        }

        @Override
        public boolean hasStableIds() {
            return false;
        }

        @Override
        public View getGroupView(int groupIndex, boolean b, View view, ViewGroup viewGroup) {
           view = inflater.inflate(android.R.layout.simple_expandable_list_item_2, null);
            TextView textView = view.findViewById(android.R.id.text1);
            textView.setText("Point date: " + groups.get(groupIndex).toString());
            return view;
        }

        @Override
        public View getChildView(int groupIndex, int childIndex, boolean b, View view, ViewGroup viewGroup) {
            view = inflater.inflate(android.R.layout.simple_list_item_2, null);
            TextView textView1 = view.findViewById(android.R.id.text1);
            TextView textView2 = view.findViewById(android.R.id.text2);
            LogData logData = db.logDao().getPointsOfTimestamps(groups.get(groupIndex)).get(childIndex);
            textView1.setText(logData.name + " (" + logData.bssid + ")");
            textView2.setText("Frequency: " + logData.frequency + "MHz   Sig. level: " + logData.signalLevel + "db");
            return view;
        }

        @Override
        public boolean isChildSelectable(int i, int i1) {
            return false;
        }
    }
}