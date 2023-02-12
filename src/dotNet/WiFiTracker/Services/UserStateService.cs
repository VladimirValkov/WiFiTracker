using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;
using WiFiTracker.DB;

namespace WiFiTracker.Services
{
    public class UserStateService
    {
        public User CurrentUser { get; set; }
        public Account CurrentAccount { get; set; }
        public UserRoles CurrentUserRole { get; set; }

        private readonly MainDB db;
        public UserStateService(MainDB _db)
        {
            db = _db;
        }
        public void Register(string accountName, string userName, string password, string email)
        {
            var acc = db.Accounts.Add(new Account()
            {
                Name= accountName,
                AccountLoginId = Guid.NewGuid().ToString().Substring(0,6),
            });
            var user = db.Users.Add(new User() 
            {
                AccoundId = acc.Entity.Id,
                UserName = userName,
                Password = password,
                Email = email,
                IsAdmin = true,
            });
            db.SaveChanges();

            Login(userName, password, acc.Entity.AccountLoginId);
        }

        public bool Login(string userName, string password, string accountLoginId)
        {
            var acc = db.Accounts.FirstOrDefault(a=>a.AccountLoginId == accountLoginId);
            var res = db.Users.FirstOrDefault(a=>a.UserName == userName && a.Password == password && acc.Id == a.AccoundId);

            if (res != null)
            {
                CurrentAccount = acc;
                CurrentUser = res;
                CurrentUserRole = db.UserRoles.FirstOrDefault(a=>a.Id == res.UserRoleId);
                return true;
            }
            else
            {
                return false;
            }
        } 

        public void Logout()
        {
            CurrentAccount = null;
            CurrentUser = null;
            CurrentUserRole = null;
        }
    }
}
