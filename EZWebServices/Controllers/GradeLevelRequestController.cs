using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace EZWebServices.Controllers
{
    public class GradeLevelRequestController : ApiController
    {
        // GET: GradeLevelRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/gradelevel/information")]
        public IHttpActionResult CreateGradeLevelInformation(GradeLevelRequest request)
        {
            var studentRequestService = new GradeLevelRequestService();
            return Ok(studentRequestService.CreateStudentBalanceInformation(request));
        }
    }
}