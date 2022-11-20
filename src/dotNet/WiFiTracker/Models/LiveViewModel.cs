using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.Models
{
    public class LiveViewModel
    {
        public string TerminalId { get; set; }
        public List<LiveTerminalData> data { get; set; }
        
        public class LiveTerminalData
        {
            public string TerminalId { get; set; }
            public string LastDate { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
        
    }
}
