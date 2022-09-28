package valkov.vladimir.wifilogger.db;

import android.content.Context;
import android.support.annotation.NonNull;

import androidx.room.AutoMigration;
import androidx.room.Database;
import androidx.room.Room;
import androidx.room.RoomDatabase;
import androidx.room.migration.Migration;
import androidx.sqlite.db.SupportSQLiteDatabase;


@Database(entities = {LogData.class}, version = 1, exportSchema = true
//        autoMigrations = {
//                //@AutoMigration(from = 1, to = 2),
//                @AutoMigration (from = 2, to = 3),
//                @AutoMigration (from = 3, to = 4),
//                @AutoMigration (from = 4, to = 5)
//}
)
public abstract class MainDB extends RoomDatabase {
    public abstract LogDao logDao();

    private static volatile MainDB instance = null;

    public static MainDB getInstance(Context context) {
        if (instance == null) {
            instance = Room.databaseBuilder(
                            context,
                            MainDB.class,
                            "maindb.db"
                    )
                    .allowMainThreadQueries() //run on main thread for brevity and convenience
                    .build();
        }
        return instance;
    }
}
