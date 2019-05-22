using System;
using System.Threading.Tasks;
using DeEekhoorn.Logic.Repositories;
using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using FluentAssertions;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NHibernate;
using Profilan.SharedKernel;

using RestSharp;

namespace EyeBoard.Tests
{

    [TestClass]
    public class DataTests
    {
        public DataTests()
        {
            //            var captureProfilerOutput = new CaptureProfilerOutput(@"");
            //            captureProfilerOutput.StartListening();

            // NHibernateProfiler.Initialize();
            
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetWeatherInfo()
        {
            var rep = new WeatherRepository();

            var weather = await rep.GetWeatherInfo(2744819);

            weather.Should().NotBeNull();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetForecastInfo()
        {
            var rep = new WeatherRepository();

            var forecast = await rep.GetForecastInfo(2744819);

            forecast.Should().NotBeNull();
        }

        [TestMethod]
        public void GetFeed()
        {
            var rep = new FeedRepository();

            var items = rep.ListByUrl("http://www.nu.nl/rss/Algemeen");

            items.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetMovies()
        {
            var rep = new MediaRepository();

            var items = rep.List();

        }

        [TestMethod]
        public void CreateMovie()
        {
            var rep = new MediaRepository();

            var item = Movie.Create("Test", DateTime.Now, DateTime.MaxValue, 0, "/movies/test.mp4");

            rep.Insert(item);
        }

        [TestMethod]
        public void CreatePresentation()
        {
            var rep = new MediaRepository();

            var item = Presentation.Create("Test", DateTime.Now, DateTime.MaxValue, 0, "/movies/test.mp4");

            rep.Insert(item);
        }

        [TestMethod]
        public void CreateScreenGroup()
        {
            var rep = new ScreenGroupRepository();

            var item = ScreenGroup.Create("Alleen Powerpoint");

            rep.Insert(item);
        }


        [TestMethod]
        public void GetGroupById()
        {
            var rep = new ScreenGroupRepository();
            var item = rep.GetById(new Guid("A42088BB-E82B-4E7A-A214-4C0C472E2910"));

            item.Should().NotBeNull();
        }

        [TestMethod]
        public void GetMediaById()
        {
            var rep = new MediaRepository();
            var item = rep.GetById(new Guid("6E982077-3C20-4126-9BF2-4E7B594C5CD1"));

            item.Should().NotBeNull();
        }

        [TestMethod]
        public void ListDeliveredOrders()
        {
            var rep = new DeliveredOrderRepository();

            var result = rep.GetTotal();

            var rep2 = new DeliveredPackageRepository();

            var result2 = rep2.GetTotal();

            // var total = rep.GetTotalDeliveredColli(2018, 50);

            // total.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CreateTask()
        {
            var rep = new TaskRepository();

            var task = EyeBoard.Logic.Models.Task.Create("Lorem", " Ipsum", "Dolor", TaskType.Video);

            rep.Insert(task);
        }

        [TestMethod]
        public void GetSpeakapMessagesByRep()
        {
            var rep = new SpeakapRepository();

            var messages = rep.List();


        }

        [TestMethod]
        public void GetSpeakapMessages()
        {
            var client = new RestClient("https://api.speakap.io/networks/2caf8309fb0004cc/messages/");
            var request = new RestRequest(Method.GET);
            
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer 2dcc4f63a00008c4_c217339e61e08a7512ab261fe3a11f6019a65a1b0e0affee4967bf2002c96ca8");
            IRestResponse response = client.Execute(request);

            SpeakapMessagesApiModel messages = JsonConvert.DeserializeObject<SpeakapMessagesApiModel>(response.Content);

            foreach (var messageLink in messages.Links.Messages)
            {
                client.BaseUrl = new Uri("https://api.speakap.io" + messageLink.Href);

                response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SpeakapMessageApiModel message = JsonConvert.DeserializeObject<SpeakapMessageApiModel>(response.Content);

                    client.BaseUrl = new Uri("https://api.speakap.io" + message.Links.Author.Href);

                    response = client.Execute(request);

                    SpeakapAuthorApiModel author = JsonConvert.DeserializeObject<SpeakapAuthorApiModel>(response.Content);

                    if (message.Links.Images != null)
                    {
                        foreach (var imageLink in message.Links.Images)
                        {
                            client.BaseUrl = new Uri("https://api.speakap.io" + imageLink.Href);

                            response = client.Execute(request);

                            SpeakapImageApiModel image = JsonConvert.DeserializeObject<SpeakapImageApiModel>(response.Content);

                            client.BaseUrl = new Uri(image.SpeakapFile.DisplayUrls.Fullscreen1080p);


                            response = client.Execute(request);
                        }
                    }
                }
            }
        }
    }
}
