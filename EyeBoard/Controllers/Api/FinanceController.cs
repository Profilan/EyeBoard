using DeEekhoorn.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Api
{
    public class FinanceController : ApiController
    {
        private readonly FinanceRepository financeRepository = new FinanceRepository();

        [HttpGet]
        [Route("api/finance/order-income")]
        public IHttpActionResult GetOrderIncome()
        {
            var item = financeRepository.GetOrderIncome();

            return Ok(item);
        }

        [HttpGet]
        [Route("api/finance/turnover")]
        public IHttpActionResult GetTurnover()
        {
            var item = financeRepository.GetTurnover();

            return Ok(item);
        }

        [HttpGet]
        [Route("api/finance/webshop-import")]
        public IHttpActionResult GetWebshopImport()
        {
            var item = financeRepository.GetWebshopImport();

            return Ok(item);
        }

        [HttpGet]
        [Route("api/finance/webshop-import-pxl")]
        public IHttpActionResult GetWebshopImportPXL()
        {
            var item = financeRepository.GetWebshopImportPXL();

            return Ok(item);
        }

        [HttpGet]
        [Route("api/finance/picked-orders")]
        public IHttpActionResult GetPickedOrders()
        {
            var item = financeRepository.GetPickedOrder();

            return Ok(item);
        }

        [HttpGet]
        [Route("api/finance/delivery")]
        public IHttpActionResult GetDelivery()
        {
            var item = financeRepository.GetDelivery();

            return Ok(item);
        }
    }
}
