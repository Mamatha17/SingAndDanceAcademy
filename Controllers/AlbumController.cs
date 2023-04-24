using Microsoft.AspNetCore.Mvc;
using SingAndDanceAcademy.Model;
using Newtonsoft.Json;
using System.Text.Json;

namespace SingAndDanceAcademy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : Controller
    {
        private string _JsonFilePath = Path.Combine(Environment.CurrentDirectory, @"ClientApp\src\assets\jsonFiles\albumList.json");

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ReadAlbumsfromJson()
        {
            using StreamReader reader = new(_JsonFilePath);
            var json = reader.ReadToEnd();
            List<AlbumData>? albumData = JsonConvert.DeserializeObject<List<AlbumData>>(json);

            return Json(albumData);
        }

        [HttpPatch]
        public JsonResult UpdateAlbumData(AlbumData item)
        {           
            try
            {
                using StreamReader reader = new(_JsonFilePath);
                var json = reader.ReadToEnd();
                reader.Close();

                List<AlbumData>? albumlist = JsonConvert.DeserializeObject<List<AlbumData>>(json);
                if (albumlist != null)
                {
                    using (StreamWriter writer = new StreamWriter(_JsonFilePath))
                    {
                        foreach (AlbumData album in albumlist)
                        {
                            if (album.Id == item.Id)
                            {
                                album.Title = item.Title;
                                album.Year = item.Year;
                                album.Genre = item.Genre;
                                album.Description = item.Description;
                                album.Action = "";

                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        String jsonString = System.Text.Json.JsonSerializer.Serialize(albumlist);
                        writer.Write(jsonString);
                        writer.Close();
                    }
                }
                return Json(albumlist);
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return Json(exp.Message);
            }
        }

        [HttpPost]
        public JsonResult AddAlbumData(AlbumData item)
        {
            try
            {
                using StreamReader reader = new(_JsonFilePath);
                var json = reader.ReadToEnd();
                reader.Close();

                List<AlbumData>? albumList = JsonConvert.DeserializeObject<List<AlbumData>>(json);
                albumList = albumList ?? new List<AlbumData>();


                using (StreamWriter writer = new StreamWriter(_JsonFilePath))
                {
                    albumList.Add(item);
                    String jsonString = System.Text.Json.JsonSerializer.Serialize(albumList);
                    writer.Write(jsonString);
                    writer.Close();
                }


                return Json(albumList);
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

                List<AlbumData>? albumList = JsonConvert.DeserializeObject<List<AlbumData>>(json);
                if (albumList != null)
                {
                    using (StreamWriter writer = new StreamWriter(_JsonFilePath))
                    {
                        AlbumData deleteitem = new AlbumData();
                        foreach (AlbumData album in albumList)
                        {
                            if (album.Id == id)
                            {
                                deleteitem = album;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        albumList.Remove(deleteitem);
                        String jsonString = System.Text.Json.JsonSerializer.Serialize(albumList);
                        writer.Write(jsonString);
                        writer.Close();
                    }
                }
                return Json(albumList);
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return Json(exp.Message);
            }
        }
    }
}
