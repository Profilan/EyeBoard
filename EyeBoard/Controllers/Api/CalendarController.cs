using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Api
{
    public class CalendarController : ApiController
    {
        private readonly CalendarRepository calendarRepository = new CalendarRepository();

        [HttpGet]
        [Route("api/calendar/events")]
        public IHttpActionResult GetEvents()
        {
            var date = DateTime.Now;
            

            var events = calendarRepository.List().Take(3);

            return Ok(events.OrderBy(x => x.Start).Where(x => x.End >= date));
        }
    }
}
