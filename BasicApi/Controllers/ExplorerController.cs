using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Serilog;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExplorerController : ControllerBase
    {

        //build
        //$ docker build -t test-expose:008 -f Dockerfile .
        //
        //volume
        //$ docker volume create --name myvol2
        //in dockerDesktop is \\wsl.localhost\docker-desktop-data\data\docker\volumes\myvol2\_data
        //in linux is /var/lib/docker/volume/myvol2/_data
        //
        //run
        //$ docker run -d -p 9595:8080 -v myvol2:/App/MyVol2 --name my-expose-8 test-expose:008
        // volume path for container must be absoute -> /App/MyVol2
        //
        //docker run -d -p 9595:8080 -v D:\Republica:/App/MyVol1 --name my-expose-8 test-expose:008
        //absolute path for host and container
        //
        //docker run -d -p 9595:8080 -v myvol2:/App/MyVol2 -v D:\Republica:/App/MyVol1 --name my-expose-8 test-expose:008
        //
        //http://localhost:9595/Explorer
        //
        public ExplorerController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            string v1 = Vol1();
            string v2 = Vol2();

            var json2 = new
            {
                resultVol1 = v1,
                resultVol2 = v2
            };

            return Ok(json2);
        }

        private string Vol1()
        {
            string absoutePath = @"/App/MyVol1";

            if (!Directory.Exists(absoutePath))
            {
                Console.WriteLine($"Directory: '{absoutePath}' not found");
                return new
                {
                    result = $"'{absoutePath}' not found"
                }.ToString();
            }

            List<string> filesList = new List<string>();

            DirectoryInfo d = new DirectoryInfo(absoutePath);
            FileInfo[] files = d.GetFiles();
            Console.WriteLine("Files in this directory.");
            Console.WriteLine("------------------------");
            foreach (FileInfo file in files)
            {
                Console.WriteLine("File Name : {0}", file.Name);
                Log.Information("File Name : {0}", file.Name);
                filesList.Add(file.Name);
            }

            var options1 = new JsonSerializerOptions
            {
                //NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
               // AllowTrailingCommas = false,
                //WriteIndented = true
            };

            //JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
            //{
            //    WriteIndented = true
            //};

            string json = JsonSerializer.Serialize(filesList, options1);

            var json2 = new
            {
                result = json
            };

            return json2.ToString();
        }

        private string Vol2()
        {
            string absoutePath = @"/App/MyVol2";

            if (!Directory.Exists(absoutePath))
            {
                Console.WriteLine($"Directory: '{absoutePath}' not found");
                return new
                {
                    result = $"'{absoutePath}' not found"
                }.ToString();
            }

            List<string> filesList = new List<string>();

            DirectoryInfo d = new DirectoryInfo(absoutePath);
            FileInfo[] files = d.GetFiles();
            Console.WriteLine("Files in this directory.");
            Console.WriteLine("------------------------");
            foreach (FileInfo file in files)
            {
                Console.WriteLine("File Name : {0}", file.Name);
                Log.Information("File Name : {0}", file.Name);
                filesList.Add(file.Name);
            }

            var options1 = new JsonSerializerOptions
            {
                //NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //AllowTrailingCommas = false,
                //WriteIndented = true
            };

            //JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
            //{
            //    WriteIndented = true
            //};

            string json = JsonSerializer.Serialize(filesList, options1);

            var json2 = new
            {
                result = json
            };

            return json2.ToString();
        }
    }
}
