using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WiFiTracker.DB
{
    public class UserRoles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int AccoundId { get; set; }

        public bool AllowTerminals { get; set; } = true;
        public bool AllowTransmitters { get; set; } = true;
        public bool AllowReportLiveView { get; set; } = true;
        public bool AllowReportTrackHistory { get; set; } = true;
        public bool AllowDownloadApp { get; set; } = true;


    }
}
