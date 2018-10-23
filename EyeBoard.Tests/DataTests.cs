using System;
using System.Threading.Tasks;
using EyeBoard.Logic.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EyeBoard.Tests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public async Task GetWeatherInfo()
        {
            var rep = new WeatherRepository();

            var weather = await rep.GetWeatherInfo(2744819);

            weather.Should().NotBeNull();
        }
    }
}
