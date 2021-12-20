using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class PaymentSchemeController : ApiController
    {
        // GET: PaymentScheme
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/paymentScheme/get_all")]
        public IEnumerable<PaymentScheme> GetPaymentScheme()
        {
            var studentAccount = new PaymentScheme();
            return studentAccount.GetPaymentScheme();
        }
    }
}