using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public IActionResult Reinport(int? fromid, int? toid)
        {
            if (fromid == null || fromid == 0 || toid == null || toid == 0)
            {
                return BadRequest(new Error() { Message = "ID is not presented." });
            }
            var logs = db.Logs.Where(a=>a.Id>=fromid && a.Id <= toid && a.Type == "api/addpoint" && a.Content.Contains("2022-11-26"));
            using (var web = new HttpClient())
            {
                foreach (var log in logs)
                {
                    var res = web.PostAsync("http://wifitracker.4every.info/Api/addpoint", new StringContent(log.Content, Encoding.UTF8,"application/json"));
                    res.Wait();
                    
                }
            }

            return Ok();
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
            dbLog("api/addpoint", body.GetRawText());

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
                if (!transmitters.Any(x => x.Bssid?.ToLower() == router.bssid?.ToLower()))
                {
                    data.routers.Remove(router);
                }
            }

            if (data.routers.Count < 3)
            {
                return Ok();
            }

            var joined = data.routers.Join(transmitters, fc => fc.bssid?.ToLower(), t => t.Bssid?.ToLower(), (fc, t) =>
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

            foreach (var item in joined)
            {
                if (item.distance > 50)
                {
                    joined.Remove(item);
                }
            }
            dbLog("api/addpoint/joined", joined);
            if (joined.Count < 3)
            {
                return Ok();
            }
            
            var result = NonlinearLeastSquares.Calculate(joined);
            if (result != null && result.latitude != 0 && result.longitude != 0)
            {
                var terminal = db.Terminals.FirstOrDefault(t => t.TerminalId == data.terminalid);
                db.Points.Add(new DB.Point()
                {
                    TerminalId = terminal.Id,
                    Latitude = result.latitude,
                    Longitude = result.longitude,
                    LogDate = DateTime.Parse(data.logdate)
                });
                db.SaveChanges();
            }
            
            return Ok();
        }


        private void dbLog<T>(string type, T content)
        {
            string contentData = "";
            if (typeof(T) == typeof(string))
            {
                contentData = content.ToString();
            }
            else
            {
               contentData = JsonSerializer.Serialize(content, typeof(T));
            }

            db.Logs.Add(new Log()
            {
                Date = DateTime.Now,
                Type = type,
                Content = contentData
            }) ;
            db.SaveChanges();
        }
    }
}
