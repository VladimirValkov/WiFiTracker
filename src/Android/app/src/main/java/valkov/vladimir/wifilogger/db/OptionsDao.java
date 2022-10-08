package valkov.vladimir.wifilogger.db;

import java.util.Date;
import java.util.List;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Query;
import androidx.room.Update;

@Dao
public interface OptionsDao {
    @Insert
    void insertOptions(Options options);

    @Update
    void updateOptions(Options options);

    @Query("SELECT * FROM Options limit 1")
    Options getOptions();
}