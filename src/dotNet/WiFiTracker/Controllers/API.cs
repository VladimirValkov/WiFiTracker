using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WiFiTracker.DB;
using WiFiTracker.MathLogic;
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
        public IActionResult AddPoint([FromBody] JsonElement body)
        {

            PointData data = JsonSerializer.Deserialize<PointData>(body.GetRawText());
            db.Logs.Add(new Log()
            {
                Date = DateTime.Now,
                Type = "api/addpoint",
                Content = body.GetRawText()
            });
            db.SaveChanges();
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

            List<Transmitter> transmitters = db.Transmitters.ToList();
            foreach (var router in data.routers.ToList())
            {
                if (!transmitters.Any(x => x.Bssid == router.bssid))
                {
                    data.routers.Remove(router);
                }
            }

            if (data.routers.Count < 3)
            {
                return Ok();
            }

            var joined = data.routers.Join(transmitters, fc => fc.bssid, t => t.Bssid, (fc, t) =>
            {
                if (fc == null)
                {
                    return null;
                }
                else
                {
                    return new NonlinearLeastSquares.Coords()
                    {
                        latitude = t.Latitude,
                        longitude = t.Longitude,
                        distance = (int)(Helpers.calculateDistance(fc.level, fc.frequency)*1000)
                    };

                }
            }).Where(x => x != null).ToList();

            var result = NonlinearLeastSquares.Calculate(joined);
            var terminal = db.Terminals.FirstOrDefault(t => t.TerminalId == data.terminalid);
            db.Points.Add(new DB.Point()
            {
                TerminalId = terminal.Id,
                Latitude = result.latitude,
                Longitude = result.longitude,
                LogDate = DateTime.Parse(data.logdate)
            });
            db.SaveChanges();

            return Ok();
        }

    }
}
