using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Models;

namespace WiFiTracker.Controllers
{

	[Authorize]
	public class LiveViewController : Controller
    {
        MainDB db;
        public LiveViewController(MainDB _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            var data = new LiveViewModel();
            data.data = db.Points.AsEnumerable()
                .GroupBy(a => a.TerminalId)
                .Select((p, g) => p.OrderBy(d => d.LogDate).Last())
                .Join(db.Terminals, p => p.TerminalId, t => t.Id, (p, t) => new LiveViewModel.LiveTerminalData()
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
