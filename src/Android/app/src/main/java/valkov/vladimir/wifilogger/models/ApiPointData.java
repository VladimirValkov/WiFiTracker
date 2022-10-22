package valkov.vladimir.wifilogger.models;

import java.util.ArrayList;
import java.util.List;

public class ApiPointData {
    String terminalid;
    String logdate;
    List<WiFiData> routers;

    public ApiPointData()
    {
        routers = new ArrayList<>();
    }

    public String getTerminalid() {
        return terminalid;
    }

    public void setTerminalid(String terminalid) {
        this.terminalid = terminalid;
    }

    public String getLogdate() {
        return logdate;
    }

    public void setLogdate(String logdate) {
        this.logdate = logdate;
    }

    public List<WiFiData> getRouters() {
        return routers;
    }

    public void setRouters(List<WiFiData> routers) {
        this.routers = routers;
    }

    public static class WiFiData
    {
        String name;

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getBssid() {
            return bssid;
        }

        public void setBssid(String bssid) {
            this.bssid = bssid;
        }

        public int getFrequency() {
            return frequency;
        }

        public void setFrequency(int frequency) {
            this.frequency = frequency;
        }

        public int getLevel() {
            return level;
        }

        public void setLevel(int level) {
            this.level = level;
        }

        String bssid;
        int frequency;
        int level;
    }
}
