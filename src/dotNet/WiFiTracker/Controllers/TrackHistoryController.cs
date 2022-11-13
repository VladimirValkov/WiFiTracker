using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Models;

namespace WiFiTracker.Controllers
{
    public class TrackHistoryController : Controller
    {
        MainDB db;
        public TrackHistoryController(MainDB _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var data = db.Points.OrderBy(a=>a.LogDate).Join(db.Terminals, p=>p.TerminalId, t=>t.Id, (p,t) => new MapPin()
                {
                    Name = t.Name + " - " + p.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    Latitude = p.Latitude.ToString().Replace(',', '.'),
                    Longitude = p.Longitude.ToString().Replace(',', '.'),
                }
            );
            return View(data.ToList());
        }
    }
}
