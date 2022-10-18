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
                if (!transmitters.Any(x=>x.Bssid == router.bssid))
                {
                    data.routers.Remove(router);
                }
            }

            if (data.routers.Count < 3)
            {
                return Ok();
            }
            var sorted_routers = data.routers.OrderBy(x => x.level).ToList();
            List<MathLogic.Point> coordinates = new List<MathLogic.Point>();
            foreach (var router in sorted_routers.ToList())
            {
                var for_calc = sorted_routers.Take(3).ToArray();
                if (for_calc.Count() < 3)
                {
                    break;
                }
                else
                {
                    var joined = for_calc.Join(transmitters, fc => fc.bssid, t => t.Bssid, (fc, t) =>
                    {
                        if (fc == null)
                        {
                            return null;
                        }
                        else
                        {
                            return new MathLogic.Point(
                            t.Latitude,
                            t.Longitude,
                            Helpers.calculateDistance(fc.level, fc.frequency));
                        }
                        }).Where(x=>x != null).ToArray();

                    double[] coords = Trilateration.Compute(joined[0], joined[1], joined[2]);
                    if (coords == null)
                    {
                        continue;
                    }
                    coordinates.Add(new MathLogic.Point(coords[0], coords[1], 0));
                    sorted_routers.RemoveAt(0);
                }
            }
            if (coordinates.Count() == 0)
            {
                return Ok();
            }
            double lat = coordinates[0].glt();
            double lon = coordinates[0].gln();
            foreach (var coord in coordinates)
            {
                lat += coord.glt();
                lat /= 2;
                lon += coord.gln();
                lon /= 2;
            }
            var terminal = db.Terminals.FirstOrDefault(t => t.TerminalId == data.terminalid);
            db.Points.Add(new DB.Point()
            {
                TerminalId = terminal.Id,
                Latitude = lat,
                Longitude = lon,
                LogDate = DateTime.Parse(data.logdate)
            });
            db.SaveChanges();
            
            return Ok();
        }
    }
}
