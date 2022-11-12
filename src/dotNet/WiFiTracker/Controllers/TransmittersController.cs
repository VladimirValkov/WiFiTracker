using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Models;

namespace WiFiTracker.Controllers
{
    public class TransmittersController : Controller
    {
        MainDB db;
        public TransmittersController(MainDB _db)
        {
            db = _db;
        }
        private Transmitter MapDataToDb(TransmitterData data)
        {
            char seperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            return new Transmitter() {
                Id = data.Id,
                Bssid = data.Bssid,
                Name = data.Name,
                Latitude = Convert.ToDouble(data.Latitude.Replace(',', seperator).Replace('.', seperator)),
                Longitude = Convert.ToDouble(data.Longitude.Replace(',', seperator).Replace('.', seperator)),
            };
        }
        public IActionResult Index()
        {
            return View(db.Transmitters.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TransmitterData data)
        {
            if (ModelState.IsValid)
            {
                db.Transmitters.Add(MapDataToDb(data));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);

        }
    }
}
