using Microsoft.AspNetCore.Authorization;
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
	public class TerminalsController : Controller
    {
        MainDB db;
        UserStateService user;
        public TerminalsController(MainDB _db, UserStateService _user)
        {
            db = _db;
            user = _user;
            
        }
        public IActionResult Index()
        { 
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if (!user.CurrentUserRole.AllowTerminals && !user.CurrentUser.IsAdmin)
            {
                return NotFound();
            }
            return View(db.Terminals.Where(a=>a.AccoundId == user.CurrentUser.AccoundId).ToList());
        }
        public IActionResult Add()
        {
            user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if (!user.CurrentUserRole.AllowTerminals && !user.CurrentUser.IsAdmin)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Terminal data)
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (ModelState.IsValid)
            {
                data.AccoundId = user.CurrentUser.AccoundId;
                db.Terminals.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
            
        }

        public IActionResult Edit(int? id)
        {
            user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if (!user.CurrentUserRole.AllowTerminals && !user.CurrentUser.IsAdmin)
            {
                return NotFound();
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var data = db.Terminals.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Terminal data)
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			if (ModelState.IsValid)
            {
				data.AccoundId = user.CurrentUser.AccoundId;
				db.Terminals.Update(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);

        }

        public IActionResult Delete(int? id)
        {
            user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if (!user.CurrentUserRole.AllowTerminals && !user.CurrentUser.IsAdmin)
            {
                return NotFound();
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var data = db.Terminals.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            db.Terminals.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
