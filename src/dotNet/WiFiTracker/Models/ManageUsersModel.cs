using System.Collections.Generic;

namespace WiFiTracker.Models
{
    public class ManageUsersModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public bool IsAdmin { get; set; }

        public int AccoundId { get; set; }

        public List<UserRoleModel> UserRoles { get; set; }

        public class UserRoleModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
