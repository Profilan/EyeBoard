﻿using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Api
{
    

    public class SpeakapController : ApiController
    {
        private readonly SpeakapRepository _speakapRepository = new SpeakapRepository();

        [HttpGet]
        [Route("api/speakap/messages")]
        public IHttpActionResult GetMessages()
        {
            var date = DateTime.Now.AddDays(-14);

            var messages = _speakapRepository.ListByAge(date).OrderByDescending(x => x.Created).Where(x => x.Created >= date);

            return Ok(messages);
        }
    }
}
