using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.Models
{
    public class TransmitterData
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Bssid { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }
    }
}
