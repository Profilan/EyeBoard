using EyeBoard.Logic.Repositories;
using EyeBoard.Areas.Admin.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Http;
using System;
using EyeBoard.Logic.Events;

namespace EyeBoard.Areas.Admin.Controllers.Api
{
    public class ScreenController : ApiController
    {
        private readonly ScreenRepository _screenRepository = new ScreenRepository();

        [HttpGet]
        [Route("api/screen")]
        public IHttpActionResult GetList()
        {
            var screenItems = _screenRepository.List();
            var screens = new List<ScreenViewModel>();
            foreach (var screen in screenItems)
            {
                screens.Add(new ScreenViewModel()
                {
                    Id = screen.Id,
                    HostName = screen.HostName,
                    IsReachable = IsHostReachable(screen.HostName)
                });
            }

            return Ok(screens);
        }

        [Route("api/Screen/Delete/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _screenRepository.Delete(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Content(HttpStatusCode.NoContent, Resources.Resources.Screen + " " + Resources.Resources.Deleted);
        }

        [HttpPost]
        [Route("api/screen/reset/{id}")]
        public IHttpActionResult Reset(Guid id)
        {
            var screen = _screenRepository.GetById(id);
            screen.Update();

            return Ok(id);
        }


        private bool IsHostReachable(string hostname)
        {

            try
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(hostname);

                if (pingReply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }
    }
}
