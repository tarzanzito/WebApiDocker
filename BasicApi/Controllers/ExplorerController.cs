using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Serilog;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExplorerController : ControllerBase
    {
        #region comments


        //NAS http://192.168.1.9:32768/explorer
        //$ cd /share/CACHEDEV1_DATA/Public/TEST

        //build
        //$ docker build -t test-expose:008 -f Dockerfile .

        //volume
        //$ docker volume create --name myvol2
        //in dockerDesktop is \\wsl.localhost\docker-desktop-data\data\docker\volumes\myvol2\_data
        //in linux is /var/lib/docker/volume/myvol2/_data

        //run
        //$ docker run -d -p 9595:8080 -v myvol2:/App/MyVol2 --name my-expose-8 test-expose:008
        // volume path for container must be absoute -> /App/MyVol2
        //
        //docker run -d -p 9595:8080 -v D:\Republica:/App/MyVol1 --name my-expose-8 test-expose:008
        //absolute path for host and container
        //
        //docker run -d -p 9595:8080 -v myvol2:/App/MyVol2 -v D:\Republica:/App/MyVol1 --name my-expose-8 test-expose:008
        //

        //docker run -d -p 9595:8080 -v myvol2:/App/MyVol2 -v D:\Republica:/App/MyVol1 --name my-expose-8 test-expose:008
        //

        //https://localhost:7113/swagger/index.html
        //https://localhost:7113
        //http://localhost:9595/Explorer

        //save image
        //docker save -o DockerImage/test-expose_008.tar test-expose:008

        //export container
        //docker export --output="latest.tar" red_panda

        #endregion

        [HttpGet]
        public IActionResult Get()
        {
            string volResult1 = GetFilesFromFolder(@"/App/MyVol1");
            string volResult2 = GetFilesFromFolder(@"/App/MyVol2");
            string volResult3 = GetFilesFromFolder(@"/App/MyVol3");

            var anonymousType = new
            {
                volume1 = volResult1,
                volume2 = volResult2,
                volume3 = volResult3
            };

            return Ok(anonymousType);
        }

        private string GetFilesFromFolder(string containerFolder) //must be absolute path
        {
            //string absoutePath = @"/App/MyVol1";

            if (!Directory.Exists(containerFolder))
            {
                Log.Information($"Directory: '{containerFolder}' not found");
                return new
                {
                    result = $"'{containerFolder}' not found"
                }.ToString();
            }

            List<string> filesList = new List<string>();

            DirectoryInfo d = new DirectoryInfo(containerFolder);
            FileInfo[] files = d.GetFiles();
            Log.Information($"Files in this directory: [{containerFolder}]");
            Log.Information("-");

            foreach (FileInfo file in files)
            {
                //Console.WriteLine("File Name : {0}", file.Name);
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

    }
}
