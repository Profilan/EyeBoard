using EyeBoard.Logic.Repositories;
using EyeBoard.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Admin.Api
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
            var items = _mediaRepository.ListByGroup(group).Where(x => x.GetType().Name == "Presentation");
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
    }
}
