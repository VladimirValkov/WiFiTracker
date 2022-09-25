package valkov.vladimir.wifilogger.db;

import androidx.room.Database;
import androidx.room.RoomDatabase;

@Database(entities = {LogData.class}, version = 2)
public abstract class MainDB extends RoomDatabase {
    public abstract LogDao logDao();
}
