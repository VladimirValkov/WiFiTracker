using Microsoft.AspNetCore.Authorization;
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
	public class LiveViewController : Controller
    {
        MainDB db;
		UserStateService user;
		public LiveViewController(MainDB _db, UserStateService _user)
        {
            db = _db;
            user = _user;
        }

        public IActionResult Index()
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
            if (!user.CurrentUserRole.AllowReportLiveView && !user.CurrentUser.IsAdmin)
            {
                return NotFound();
            }
			var data = new LiveViewModel();
            var terminals = db.Terminals.Where(a => a.AccoundId == user.CurrentUser.AccoundId).ToList();
            data.data = db.Points.AsEnumerable()
                .GroupBy(a => a.TerminalId)
                .Select((p, g) => p.OrderBy(d => d.LogDate).Last())
                .Join(terminals, p => p.TerminalId, t => t.Id, (p, t) => new LiveViewModel.LiveTerminalData()
                {
                    TerminalId = t.Name,
                    LastDate = p.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    Latitude = p.Latitude.ToString(),
                    Longitude = p.Longitude.ToString()
                }).ToList();


            return View(data);
        }
    }
}
