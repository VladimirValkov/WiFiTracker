using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;
using WiFiTracker.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WiFiTracker.Services
{
    public class UserStateService
    {
        public User CurrentUser { get; set; }
        public Account CurrentAccount { get; set; }
        public UserRoles CurrentUserRole { get; set; }

        private readonly MainDB db;
        private readonly IHttpContextAccessor http;
        public UserStateService(MainDB _db, IHttpContextAccessor _httpContextAccessor)
        {
            db = _db;
            http = _httpContextAccessor;
        }
        public string Register(string accountName, string userName, string password, string email)
        {
            var acc = db.Accounts.Add(new Account()
            {
                Name= accountName,
                AccountLoginId = Guid.NewGuid().ToString().Substring(0,6),
            });
            db.SaveChanges();
            var created_acc = db.Accounts.OrderBy(a=>a.Id).Last();
            var user = db.Users.Add(new User() 
            {
                AccoundId = created_acc.Id,
                UserName = userName,
                Password = password,
                Email = email,
                IsAdmin = true,
            });
            db.SaveChanges();
            return created_acc.AccountLoginId;
        }

        public async Task<bool> Login(string userName, string password, string accountLoginId)
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

        public void LoadLoggedUserData(string user_id)
        {
			CurrentAccount = null;
			CurrentUser = null;
			CurrentUserRole = null;

			
            if (user_id != null)
            {
                CurrentUser = db.Users.FirstOrDefault(a => a.Id == int.Parse(user_id));
                CurrentAccount = db.Accounts.FirstOrDefault(a => a.Id == CurrentUser.AccoundId);
                if (CurrentUser.IsAdmin)
                {
                    CurrentUserRole = new UserRoles();  
                }
                else
                {
                    CurrentUserRole = db.UserRoles.FirstOrDefault(a => a.Id == CurrentUser.UserRoleId);
                }
                
            }
        }
    }
}
