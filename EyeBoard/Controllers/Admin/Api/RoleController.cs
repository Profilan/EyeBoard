using EyeBoard.Models.Identity;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Profilan.SharedKernel;
using EyeBoard.Logic.Repositories;

namespace EyeBoard.Controllers.Admin.Api
{
    public class RoleController : ApiController
    {
        private RoleRepository _roleRepository;

        public RoleController()
        {
           
        }

        public RoleController(RoleRepository roleRepository)
        {
            RoleRepository = roleRepository;

        }

        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ?? new RoleRepository();
            }
            private set
            {
                _roleRepository = value;
            }
        }

        [HttpGet]
        [Route("api/role/{searchstring}")]
        public IHttpActionResult GetRolesBySearchstring(string searchstring)
        {
            return Ok(RoleRepository.ListBySearchstring(searchstring));
        }
    }
}
