// <copyright file="Startup.cs" company="Principal33">
// Copyright (c) Principal33. All rights reserved.
// </copyright>

using HelloWorldWeb.Controllers;
using Microsoft.Extensions.Configuration;

namespace HelloWorldWeb
{
    public class WeatherControllerSettings : IWeatherControllerSettings
    {
        public WeatherControllerSettings(IConfiguration conf)
        {
            this.ApiKey = conf["WeatherForecast:ApiKey"];
            this.Longitude = conf["WeatherForecast:Longitude"];
            this.Latitude = conf["WeatherForecast:Latitude"];
        }

        public string Longitude { get; set; }

        public string ApiKey { get; set; }

        public string Latitude { get; set; }
    }
}