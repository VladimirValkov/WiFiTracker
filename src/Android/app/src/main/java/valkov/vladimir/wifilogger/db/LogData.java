package valkov.vladimir.wifilogger.db;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;
import androidx.room.TypeConverter;
import androidx.room.TypeConverters;

@Entity
public class LogData {
    @PrimaryKey(autoGenerate = true)
    public int id;

    @ColumnInfo(name = "log_timestamp", index = true)
    @TypeConverters(TimestampConverter.class)
    public Date logTimeStamp;

    @ColumnInfo(name = "bssid")
    public String bssid;

    @ColumnInfo(name = "frequency")
    public int frequency;

    @ColumnInfo(name = "signal_level")
    public int signalLevel;

    @ColumnInfo(name = "name")
    public String name;
}
