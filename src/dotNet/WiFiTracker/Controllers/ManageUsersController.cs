using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Models;
using WiFiTracker.Services;

namespace WiFiTracker.Controllers
{

	[Authorize]
	public class ManageUsersController : Controller
	{
		MainDB db;
		UserStateService user;
		public ManageUsersController(MainDB _db, UserStateService _user)
		{
			db = _db;
			user = _user;

		}
		public IActionResult Index()
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (!user.CurrentUser.IsAdmin)
			{
				return NotFound();
			}
			var data = db.Users.Where(a => a.AccoundId == user.CurrentUser.AccoundId).ToList();
			foreach (var item in data)
			{
				item.Password = "********";
			}
            return View(data);
		}

		public IActionResult Add()
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (!user.CurrentUser.IsAdmin)
			{
				return NotFound();
			}
			var data = db.UserRoles.Where(a => a.AccoundId == user.CurrentUser.AccoundId).Select(a => new ManageUsersModel.UserRoleModel
			{
				Id = a.Id,
				Name = a.Name
			}).ToList();
			var output = new ManageUsersModel()
			{
				UserRoles = data,
			};
			return View(output);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(ManageUsersModel data)
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (ModelState.IsValid)
			{
				data.AccoundId = user.CurrentUser.AccoundId;
				var dbddata = new User()
				{
					AccoundId = data.AccoundId,
					UserName = data.UserName,
					Password = data.Password,
					Email = data.Email,
					UserRoleId = data.UserRoleId,
					IsAdmin = data.IsAdmin
				};
				db.Users.Add(dbddata);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
				return View(data);

		}

		public IActionResult Edit(int? id)
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (id == null || id == 0 || !user.CurrentUser.IsAdmin)
			{
				return NotFound();
			}
			var data = db.Users.Find(id);
			if (data == null)
			{
				return NotFound();
			}
            var dbdata = db.UserRoles.Where(a => a.AccoundId == user.CurrentUser.AccoundId).Select(a => new ManageUsersModel.UserRoleModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
            var output = new ManageUsersModel()
            {
				Id = data.Id,
                UserRoles = dbdata,
                AccoundId = user.CurrentUser.AccoundId,
                UserName = data.UserName,
                Password = data.Password,
                Email = data.Email,
                UserRoleId = data.UserRoleId,
                IsAdmin = data.IsAdmin
            };
            return View(output);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ManageUsersModel data)
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (ModelState.IsValid)
			{
                data.AccoundId = user.CurrentUser.AccoundId;
                var dbdata = new User()
                {
					Id = data.Id,
                    AccoundId = data.AccoundId,	
                    UserName = data.UserName,
                    Password = data.Password,
                    Email = data.Email,
                    UserRoleId = data.UserRoleId,
                    IsAdmin = data.IsAdmin
                };
				db.Users.Update(dbdata);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(data);

		}

		public IActionResult Delete(int? id)
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (id == null || id == 0 || !user.CurrentUser.IsAdmin)
			{
				return NotFound();
			}
			var data = db.Users.Find(id);
			if (data == null)
			{
				return NotFound();
			}
			db.Users.Remove(data);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
