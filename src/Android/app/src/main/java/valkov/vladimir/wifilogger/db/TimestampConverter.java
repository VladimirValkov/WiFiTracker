package valkov.vladimir.wifilogger.db;

import android.provider.SyncStateContract;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import androidx.room.ProvidedTypeConverter;
import androidx.room.TypeConverter;

@ProvidedTypeConverter
public class TimestampConverter {
    static DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

//    @TypeConverter
//    public static Date fromTimestamp(String value) {
//        if (value != null) {
//            try {
//                return df.parse(value);
//            } catch (ParseException e) {
//                e.printStackTrace();
//            }
//            return null;
//        } else {
//            return null;
//        }
//    }
    @TypeConverter
    public static Date fromTimestamp(Long value) {
        return value == null ? null : new Date(value);
    }

    @TypeConverter
    public static Long dateToTimestamp(Date date) {
        return date == null ? null : date.getTime();
    }

}