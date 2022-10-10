using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EyeBoard.Controllers.Api
{
    public class StockMarketController : ApiController
    {
        [HttpGet]
        [Route("api/stock-market/aex")]
        public IHttpActionResult GetAex()
        {
            var options = new RestClientOptions("https://www.google.com/async/finance_wholepage_price_updates?ei=gDuEXvivGOTdxgOrw62wBg&rlz=1C1GCEU_nlNL884NL885&yv=3&async=mids:%2Fm%2F04xjhg,currencies:,_fmt:jspb");
            var client = new RestClient(options);
            var request = new RestRequest();
            var response = client.Get(request);

            return Ok(response.Content);
        }

        [HttpGet]
        [Route("api/stock-market/usd")]
        public IHttpActionResult GetUsd()
        {
            var options = new RestClientOptions("https://fcsapi.com/api-v2/forex/latest?id=1&access_key=I2v4XRzrCAXR2oNBrjWc7MKm437Q6u3AYSLx1dlviYJH7h7");
            var client = new RestClient(options);
            // var client = new RestClient("https://api.exchangeratesapi.io/latest?base=EUR");
            var request = new RestRequest();
            var response = client.Execute(request);

            return Ok(response.Content);
        }
    }
}
