using DeEekhoorn.Logic.Repositories;
using EyeBoard.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Api
{
    public class DeliveredPackageController : ApiController
    {
        private readonly DeliveredPackageRepository _deliveredPackageRepository = new DeliveredPackageRepository();

        [HttpGet]
        [Route("api/delivered-package/total")]
        public IHttpActionResult GetTotal()
        {
            var currentDate = DateTime.Now;

            var total = _deliveredPackageRepository.GetTotal();

            var view = new DeliveredPackageViewModel()
            {
                TotalDeliveredPackages = total
            };

            return Ok(view);
        }
    }
}
