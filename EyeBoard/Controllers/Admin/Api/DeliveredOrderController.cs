using DeEekhoorn.Logic.Repositories;
using EyeBoard.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Admin.Api
{
    public class DeliveredOrderController : ApiController
    {
        private readonly DeliveredOrderRepository _deliveredOrderRepository = new DeliveredOrderRepository();

        [HttpGet]
        [Route("api/delivered-order/total")]
        public IHttpActionResult GetTotal()
        {
            var currentDate = DateTime.Now;

            var total = _deliveredOrderRepository.GetTotalDeliveredColli();

            var view = new DeliveredOrderViewModel()
            {
                TotalDeliveredColli = total
            };

            return Ok(view);
        }
    }
}
