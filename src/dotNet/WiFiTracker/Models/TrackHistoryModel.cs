using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WiFiTracker.DB;

namespace WiFiTracker.Models
{
    public class TrackHistoryModel
    {
        
        public string TerminalId { get; set; }

        [Required]
        public string DateFrom { get; set; }

        [Required]
        public string DateTo { get; set; }
        public List<MapPin> Result { get; set; }
        public List<Terminal> Terminals { get; set; }
        public List<Transmitter> Transmitters { get; set; }

        public TrackHistoryModel()
        {
            Result = new List<MapPin>();
            Terminals = new List<Terminal>();
        }
    }
}
