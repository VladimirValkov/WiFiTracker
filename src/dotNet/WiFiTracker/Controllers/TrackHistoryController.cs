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
	public class TrackHistoryController : Controller
    {
        MainDB db;
		UserStateService user;
		public TrackHistoryController(MainDB _db, UserStateService _user)
        {
            db = _db;
			user = _user;
		}
        public IActionResult Index()
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			//var data = db.Points.OrderBy(a=>a.LogDate).Join(db.Terminals, p=>p.TerminalId, t=>t.Id, (p,t) => new MapPin()
			//    {
			//        Name = t.Name + " - " + p.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
			//        Latitude = p.Latitude.ToString().Replace(',', '.'),
			//        Longitude = p.Longitude.ToString().Replace(',', '.'),
			//    }
			//);
			var data = new TrackHistoryModel()
            {
                Terminals = db.Terminals.Where(a=>a.AccoundId == user.CurrentUser.AccoundId).ToList(),
                Transmitters = db.Transmitters.Where(a => a.AccoundId == user.CurrentUser.AccoundId).ToList()
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(TrackHistoryModel data)
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			data.Result = db.Points.Where(a=>a.AccoundId == user.CurrentUser.AccoundId).Where(a => a.LogDate >= DateTime.ParseExact(data.DateFrom, "dd.MM.yyyy HH:mm", null) && a.LogDate <= DateTime.ParseExact(data.DateTo, "dd.MM.yyyy HH:mm", null) && a.TerminalId == int.Parse(data.TerminalId))
                .OrderBy(a => a.LogDate)
                .Join(db.Terminals, p => p.TerminalId, t => t.Id, (p, t) => new MapPin()
                {
                    Name = t.Name + " - " + p.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    Latitude = p.Latitude.ToString().Replace(',', '.'),
                    Longitude = p.Longitude.ToString().Replace(',', '.'),
                    
                }).ToList();
            data.Terminals = db.Terminals.Where(a => a.AccoundId == user.CurrentUser.AccoundId).ToList();
            data.Transmitters = db.Transmitters.Where(a => a.AccoundId == user.CurrentUser.AccoundId).ToList();
            return View(data);
        }
    }
}
