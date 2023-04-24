using Microsoft.AspNetCore.Mvc;
using SingAndDanceAcademy.Model;
using Newtonsoft.Json;
using System.Text.Json;

namespace SingAndDanceAcademy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertController : Controller
    {
        private string _JsonFilePath = Path.Combine(Environment.CurrentDirectory, @"ClientApp\src\assets\jsonFiles\concertList.json");
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ReadConcertsfromJson()
        {
            using StreamReader reader = new(_JsonFilePath);
            var json = reader.ReadToEnd();
            List<ConcertData>? concertData = JsonConvert.DeserializeObject<List<ConcertData>>(json);

            return Json(concertData);
        }

        [HttpPatch]
        public JsonResult UpdateConcertData(ConcertData item)
        {
            try
            {
                using StreamReader reader = new(_JsonFilePath);
                var json = reader.ReadToEnd();
                reader.Close();

                List<ConcertData>? concertList = JsonConvert.DeserializeObject<List<ConcertData>>(json);
                if (concertList != null)
                {
                    using (StreamWriter writer = new StreamWriter(_JsonFilePath))
                    {
                        foreach (ConcertData concert in concertList)
                        {
                            if (concert.Id == item.Id)
                            {
                                concert.Id = item.Id;
                                concert.Name = item.Name;
                                concert.Composer = item.Composer;
                                concert.Date = item.Date;
                                concert.Duration = item.Duration;
                                concert.Location = item.Location;
                                concert.Action = "";
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        String jsonString = System.Text.Json.JsonSerializer.Serialize(concertList);
                        writer.Write(jsonString);
                        writer.Close();
                    }
                }
                return Json(concertList);
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return Json(exp.Message);
            }
        }

        [HttpPost]
        public JsonResult AddConcertData(ConcertData item)
        {
            try
            {
                using StreamReader reader = new(_JsonFilePath);
                var json = reader.ReadToEnd();
                reader.Close();

                List<ConcertData>? concertList = JsonConvert.DeserializeObject<List<ConcertData>>(json);
                concertList = concertList ?? new List<ConcertData>();


                using (StreamWriter writer = new StreamWriter(_JsonFilePath))
                {
                    concertList.Add(item);
                    String jsonString = System.Text.Json.JsonSerializer.Serialize(concertList);
                    writer.Write(jsonString);
                    writer.Close();
                }


                return Json(concertList);
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return Json(exp.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public JsonResult DeleteData(int id)
        {
            try
            {
                using StreamReader reader = new(_JsonFilePath);
                var json = reader.ReadToEnd();
                reader.Close();

                List<ConcertData>? concertList = JsonConvert.DeserializeObject<List<ConcertData>>(json);
                if (concertList != null)
                {
                    using (StreamWriter writer = new StreamWriter(_JsonFilePath))
                    {
                        ConcertData deleteitem = new ConcertData();
                        foreach (ConcertData concert in concertList)
                        {
                            if (concert.Id == id)
                            {
                                deleteitem = concert;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        concertList.Remove(deleteitem);
                        String jsonString = System.Text.Json.JsonSerializer.Serialize(concertList);
                        writer.Write(jsonString);
                        writer.Close();
                    }
                }
                return Json(concertList);
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return Json(exp.Message);
            }
        }

    }
}
