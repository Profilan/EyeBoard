using EyeBoard.Logic.Repositories;
using EyeBoard.Models.Api;
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
    public class PresentationController : ApiController
    {
        private readonly MediaRepository _mediaRepository = new MediaRepository();
        private readonly ScreenGroupRepository _groupRepository = new ScreenGroupRepository();

        [HttpGet]
        [Route("api/presentation/{groupId}")]
        public IHttpActionResult GetList(Guid groupId)
        {
            var group = _groupRepository.GetById(groupId);
            var items = _mediaRepository.ListByGroup(group).Where(x => x.GetType().Name == "Presentation" || x.GetType().Name == "Movie");
            var presentations = new List<PresentationModel>();
            foreach (var item in items)
            {
                presentations.Add(new PresentationModel()
                {
                    Id = item.Id,
                    Url = item.Url
                });
            }

            return Ok(presentations);
        }

        [HttpPost]
        [Route("api/presentation/delete/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var presentation = _mediaRepository.GetById(id);
                 
                var physicalFile = HttpContext.Current.Server.MapPath(presentation.Url);
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
