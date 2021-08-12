using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWeb.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable SA1101 // Prefix local calls with this

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HelloWorldWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly string latitude = "46.7700";
        private readonly string longitude = "23.5800";
        private readonly string apiKey = "8cd97ba68fce7f8e6a81989651609eaf";

        // GET: api/<WeatherController>
        [HttpGet]
        public IEnumerable<DailyWeatherRecord> Get()
        {
            // lat 46.7700 lon 23.5800
            // https://api.openweathermap.org/data/2.5/onecall?lat=46.7700&lon=23.5800&exclude=hourly,minutely&appid=8cd97ba68fce7f8e6a81989651609eaf
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return this.ConvertResponseToWeatherRecordList(response.Content);
        }

        public IEnumerable<DailyWeatherRecord> ConvertResponseToWeatherRecordList(string content)
        {
            return new DailyWeatherRecord[]
            {
                new DailyWeatherRecord(new DateTime(2021, 08, 12), 22.0f, WeatherType.Mild),
                new DailyWeatherRecord(new DateTime(2021, 08, 12), 25.0f, WeatherType.Mild),
            };
        }

        // GET api/<WeatherController>/5
        [HttpGet("{id}")]
#pragma warning disable SA1202 // Elements should be ordered by access
        public string Get(int id)
        {
            return "value";
        }
    }
}
