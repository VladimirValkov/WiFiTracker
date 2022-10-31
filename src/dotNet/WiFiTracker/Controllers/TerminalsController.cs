using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiFiTracker.DB;

namespace WiFiTracker.Controllers
{
    public class TerminalsController : Controller
    {
        MainDB db;
        public TerminalsController(MainDB _db)
        {
            db = _db;
        }
        public IActionResult All()
        {
            return View(db.Terminals.ToList());
        }
        public IActionResult Terminal()
        {
            
            return View();
        }

        //TODO Change All() to Index()
    }
}
