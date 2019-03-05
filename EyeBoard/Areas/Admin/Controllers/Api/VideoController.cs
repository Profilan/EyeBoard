using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EyeBoard.Areas.Admin.Controllers.Api
{
    public class VideoController : ApiController
    {
        private readonly MediaRepository _mediaRepository = new MediaRepository();

        [HttpPost]
        [Route("api/video/delete/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var video = _mediaRepository.GetById(id);

                var physicalFile = HttpContext.Current.Server.MapPath(video.Url);
                File.Delete(physicalFile);

                _mediaRepository.Delete(id);

                return Ok(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
