using System;
using System.Threading.Tasks;
using DeEekhoorn.Logic.Repositories;
using EyeBoard.Logic.Models;
using EyeBoard.Logic.Repositories;
using FluentAssertions;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Profilan.SharedKernel;

namespace EyeBoard.Tests
{

    [TestClass]
    public class DataTests
    {
        public DataTests()
        {
            //            var captureProfilerOutput = new CaptureProfilerOutput(@"");
            //            captureProfilerOutput.StartListening();

            NHibernateProfiler.Initialize();
            
        }

        [TestMethod]
        public async Task GetWeatherInfo()
        {
            var rep = new WeatherRepository();

            var weather = await rep.GetWeatherInfo(2744819);

            weather.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetForecastInfo()
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
        public void ListRolesBySearchstring()
        {
            var rep = new RoleRepository();

            var items = rep.ListBySearchstring("sup");

            items.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void AddToRoleUserToRole()
        {

            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var user = session.QueryOver<User>()
                       .Where(r => r.UserName == "Administrator")
                       .SingleOrDefault();
                    var role = session.QueryOver<Role>()
                        .Where(r => r.Name == "superuser")
                        .SingleOrDefault();


                    if (role != null)
                    {
                       

                        user.Roles.Clear();
                        user.Roles.Add(role);

                        session.SaveOrUpdate(user);
                        transaction.Commit();

                    }
                }
            }
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
        public void GetUserById()
        {
            var rep = new UserRepository();
            var item = rep.GetById(1);

            item.Should().NotBeNull();
        }

        [TestMethod]
        public void ListDeliveredOrders()
        {
            var rep = new DeliveredOrderRepository();

            var total = rep.GetTotalDeliveredColli(2018, 50);

            total.Should().BeGreaterThan(0);
        }


    }
}
