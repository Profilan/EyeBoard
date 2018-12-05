using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EyeBoard.Controllers.Admin.Api
{
    public class FeedController : ApiController
    {
        private readonly FeedRepository _feedRepository = new FeedRepository();

        [HttpGet]
        [Route("api/feed")]
        public IHttpActionResult GetList(string url)
        {
            var feeds = _feedRepository.ListByUrl(HttpContext.Current.Server.UrlDecode(url));

            return Ok(feeds);
        }
    }
}
