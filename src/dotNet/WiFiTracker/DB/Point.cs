
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.DB
{
    public class Point
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public int TerminalId { get; set; }
        public DateTime LogDate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
