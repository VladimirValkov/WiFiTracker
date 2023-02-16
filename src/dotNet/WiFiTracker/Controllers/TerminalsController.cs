using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiFiTracker.DB;

namespace WiFiTracker.Controllers
{

	[Authorize]
	public class TerminalsController : Controller
    {
        MainDB db;
        public TerminalsController(MainDB _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Terminals.ToList());
        }
        public IActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Terminal data)
        {
            if (ModelState.IsValid)
            {
                db.Terminals.Add(data);
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
            if (ModelState.IsValid)
            {
                db.Terminals.Update(data);
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
