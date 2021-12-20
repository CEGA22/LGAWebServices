using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class TransactionHistoryController : ApiController
    {
        // GET: TransactionHistory
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/transactionHistory_get_all")]
        public IEnumerable<TransactionHistoryRequest> GetTransactionHistory()
        {
            var studentAccount = new TransactionHistoryRequestService();
            return studentAccount.GetTransactionHistory();
        }


        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/student/transactionHistory")]
        public IHttpActionResult CreateStudentTransactionHistory(TransactionHistoryRequest request)
        {
            var studentRequestService = new TransactionHistoryRequestService();
            return Ok(studentRequestService.CreateStudentBalanceInformation(request));
        }
    }
}