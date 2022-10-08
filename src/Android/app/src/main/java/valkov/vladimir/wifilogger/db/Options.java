package valkov.vladimir.wifilogger.db;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity
public class Options {
    @PrimaryKey(autoGenerate = true)
    public int id;

    @ColumnInfo(name = "server_url")
    public String serverURL;

    @ColumnInfo(name = "identifier")
    public String identifier;
}
