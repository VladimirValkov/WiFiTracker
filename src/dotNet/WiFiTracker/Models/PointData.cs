using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.Models
{
    public class PointData
    {
        public string terminalid { get; set; }
        public DateTime logdate  { get; set; }
        public List<WiFiData> routers { get; set; }
        public class WiFiData
        {
            public string name { get; set; }
            public string bssid{ get; set; }
            public int frequency { get; set; }
            public int level { get; set; }
        }
    }
}
