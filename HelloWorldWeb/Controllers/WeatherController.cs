using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable SA1101 // Prefix local calls with this

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HelloWorldWeb.Controllers
{
    /// <summary>
    /// Fetch data from weather API.
    /// <see href="https://openweathermap.org/api">Weather API</see>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly string latitude;
        private readonly string longitude;
        private readonly string apiKey;

        public WeatherController(IWeatherControllerSettings conf)
        {
            longitude = conf.Longitude;
            latitude = conf.Latitude;
            apiKey = conf.ApiKey;
        }

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

        /// <summary>
        /// Get a weather forecast for the day in specified amount of days from now.
        /// </summary>
        /// <param name="index">Amount of days from now (from 0 to 7).</param>
        /// <returns>The weather forecast.</returns>
        [HttpGet("{index}")]
        public DailyWeatherRecord Get(int index)
        {
            return Get().ElementAt(index);
        }

        [NonAction]
        public IEnumerable<DailyWeatherRecord> ConvertResponseToWeatherRecordList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"].Take(7);
            return jsonArray.Select(CreateDailyWeatherRecordFromJToken);
        }

        private DailyWeatherRecord CreateDailyWeatherRecordFromJToken(JToken item)
        {
            long unixDateTime = item.Value<long>("dt");
            var day = DateTimeOffset.FromUnixTimeSeconds(unixDateTime).DateTime.Date;
            var temperature = item.SelectToken("temp").Value<float>("day") - 273.15f;
            string weather = item.SelectToken("weather")[0].Value<string>("description");
            var type = Convert(weather);
            DailyWeatherRecord dailyWeatherRecord = new DailyWeatherRecord(day, temperature, type);
            return dailyWeatherRecord;
        }

        private WeatherType Convert(string weather)
        {
            switch (weather)
            {
                case "few clouds":
                    return WeatherType.FewClouds;
                case "light rain":
                    return WeatherType.LightRain;
                case "broken clouds":
                    return WeatherType.BrokenClouds;
                case "scattered clouds":
                    return WeatherType.ScatteredClouds;
                case "overcast clouds":
                    return WeatherType.OvercastClouds;
                case "clear sky":
                    return WeatherType.ClearSky;
                case "moderate rain":
                    return WeatherType.ModerateRain;
                default:
                    throw new Exception($"Unknown weather type {weather}.");
            }
        }
#pragma warning disable SA1202 // Elements should be ordered by access
    }
}
