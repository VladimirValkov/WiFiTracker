using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.Models;

namespace WiFiTracker.Controllers
{
    public class API : Controller
    {
        MainDB db;
        public API(MainDB _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult Test(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new Error() { Message = "ID is not presented." });
            }

            if (!db.Terminals.Any(t=>t.TerminalId==id))
            {
                return BadRequest(new Error() { Message = "ID doesn't match." });
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddPoint(PointData data)
        {
            if (data == null)
            {
                return BadRequest(new Error() { Message = "Data is not presented." });
            }

            if (string.IsNullOrEmpty(data.terminalid))
            {
                return BadRequest(new Error() { Message = "ID is not presented." });
            }

            if (!db.Terminals.Any(t => t.TerminalId == data.terminalid))
            {
                return BadRequest(new Error() { Message = "ID doesn't match." });
            }

            //MathLogic.Point p1 = new MathLogic.Point(-19.6685, -69.1942, 84);
            //Point p2 = new Point(-20.2705, -70.1311, 114);
            //Point p3 = new Point(-20.5656, -70.1807, 120);
            //double[] a = Trilateration.Compute(p1, p2, p3);
            //Console.WriteLine("LatLon: " + a[0] + ", " + a[1]);

            return Ok();
        }
    }
}
