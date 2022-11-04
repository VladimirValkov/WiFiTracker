using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.DB
{
    public class Terminal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        
        [StringLength(100)]
        [Required]
        public string TerminalId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }


    }
}
