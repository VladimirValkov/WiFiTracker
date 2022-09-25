package valkov.vladimir.wifilogger.db;
import java.util.Date;
import java.util.List;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Query;
import androidx.room.TypeConverters;

@Dao
@TypeConverters(TimestampConverter.class)
public interface LogDao {

    @Insert
    void insertPoint(LogData point);

    @Delete
    void deletePoint(LogData point);

    @Query("SELECT DISTINCT log_timestamp FROM LogData")
    List<Date> getTimestamps();

    @Query("SELECT * FROM LogData WHERE log_timestamp = :logTimestamp")
    List<LogData> getPointsOfTimestamps(Date logTimestamp);
}
