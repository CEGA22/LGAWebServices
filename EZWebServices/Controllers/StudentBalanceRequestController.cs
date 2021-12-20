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
    public class StudentBalanceRequestController : ApiController
    {
        // GET: StudentBalanceRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/studentBalance/information")]
        public IHttpActionResult CreateStudentBalanceInformation(StudentBalanceRequest request)
        {
            var studentRequestService = new StudentBalanceRequestService();
            return Ok(studentRequestService.CreateStudentBalanceInformation(request));
        }
    }
}