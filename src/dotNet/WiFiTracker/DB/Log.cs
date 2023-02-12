using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.DB
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        public string Content { get; set; }

        public int AccoundId { get; set; }
    }
}
