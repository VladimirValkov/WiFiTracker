using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Models;
using WiFiTracker.Services;

namespace WiFiTracker.Controllers
{
	[Authorize]
	public class TransmittersController : Controller
    {
        MainDB db;
		UserStateService user;
		public TransmittersController(MainDB _db, UserStateService _user)
        {
            db = _db;
			user = _user;
		}
        private Transmitter MapDataToDb(TransmitterData data)
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			char seperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            return new Transmitter() {
                Id = data.Id,
                Bssid = data.Bssid,
                Name = data.Name,
                Latitude = Convert.ToDouble(data.Latitude.Replace(',', seperator).Replace('.', seperator)),
                Longitude = Convert.ToDouble(data.Longitude.Replace(',', seperator).Replace('.', seperator)),
                AccoundId = user.CurrentUser.AccoundId
            };
        }
        private TransmitterData DbToMapData(Transmitter data)
        {
            return new TransmitterData()
            {
                Id = data.Id,
                Bssid = data.Bssid,
                Name = data.Name,
                Latitude = data.Latitude.ToString(),
                Longitude = data.Longitude.ToString(),
            };
        }
        public IActionResult Index()
        {
			user.LoadLoggedUserData(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
			return View(db.Transmitters.Where(a=>a.AccoundId == user.CurrentUser.AccoundId).ToList());
        }

        public IActionResult Add()
        {
            var data = new TransmitterData();
            var lastData = db.Transmitters.OrderBy(a=>a.Id).Last();
            data.LastLatitude = lastData.Latitude.ToString();
            data.LastLongitude = lastData.Longitude.ToString();
            return View(data);
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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var data = db.Transmitters.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(DbToMapData(data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransmitterData data)
        {
            if (ModelState.IsValid)
            {
                Transmitter dbData;
                try
                {
                    dbData = MapDataToDb(data);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Latitude", "Latitude or Longitude value is not in correct format.");
                    return View(data);
                }
                db.Transmitters.Update(dbData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var data = db.Transmitters.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            db.Transmitters.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
