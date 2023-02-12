using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WiFiTracker.DB
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public bool IsAdmin { get; set; }

        public int AccoundId { get; set; }
    }
}
