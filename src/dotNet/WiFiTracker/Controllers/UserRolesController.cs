﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Services;

namespace WiFiTracker.Controllers
{

	[Authorize]
	public class UserRolesController : Controller
	{
		MainDB db;
		UserStateService user;
		public UserRolesController(MainDB _db, UserStateService _user)
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
			return View(db.UserRoles.Where(a=>a.AccoundId == user.CurrentUser.AccoundId).ToList());
		}
		
		public IActionResult Add()
		{
            user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if (!user.CurrentUser.IsAdmin)
            {
                return NotFound();
            }
            return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(UserRoles data)
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (ModelState.IsValid)
			{
				data.AccoundId = user.CurrentUser.AccoundId;
				db.UserRoles.Add(data);
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
			var data = db.UserRoles.Find(id);
			if (data == null)
			{
				return NotFound();
			}
			return View(data);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(UserRoles data)
		{
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (ModelState.IsValid)
			{
				data.AccoundId = user.CurrentUser.AccoundId;
				db.UserRoles.Update(data);
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
			var data = db.UserRoles.Find(id);
			if (data == null)
			{
				return NotFound();
			}
			db.UserRoles.Remove(data);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}