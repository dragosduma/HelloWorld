using HelloWorldWeb.Controllers;
using HelloWorldWeb.Models;
using Moq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldWeb.Tests
{
    public class WeatherControllerTests
    {
        [Fact]
        public void TestCheckingConversion()
        {
            //Assume
            string content = LoadJsonFromResource();
            var weatherControllerSettingsMock = new Mock<IWeatherControllerSettings>();
            WeatherController weatherController = new WeatherController(weatherControllerSettingsMock.Object);
            //Act
            var result = weatherController.ConvertResponseToWeatherRecordList(content);
            //Assert
            Assert.Equal(7, result.Count());
            var firstDay = result.First();

            Assert.Equal(new DateTime(2021, 08, 12), firstDay.Day);
            Assert.Equal(24.260009765625, firstDay.Temperature);
            Assert.Equal(WeatherType.FewClouds, firstDay.Type);
        }

        private string LoadJsonFromResource() 
        {
            var resourceName = "HelloWorldWeb.Tests.TestData.ContentWeatherApi.json";
            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var tr = new StreamReader(resourceStream))
            {
                return tr.ReadToEnd();
            }
                
        }
    }
}
