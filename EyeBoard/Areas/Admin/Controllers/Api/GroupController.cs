using EyeBoard.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Areas.Admin.Controllers.Api
{
    public class GroupController : ApiController
    {

        private readonly ScreenGroupRepository _groupRepository = new ScreenGroupRepository();

        [Route("api/Group/Delete/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _groupRepository.Delete(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Content(HttpStatusCode.NoContent, Resources.Resources.Group + " " + Resources.Resources.Deleted);
        }
    }
}
