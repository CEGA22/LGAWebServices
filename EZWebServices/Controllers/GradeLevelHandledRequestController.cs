using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class GradeLevelHandledRequestController : ApiController
    {
        // GET: GradeLevelHandledRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/gradelevelhandled/information")]
        public IHttpActionResult CreateGradeLevelHandledInformation(GradeLevelHandledRequest request)
        {
            var studentRequestService = new GradeLevelHandledRequestService();
            return Ok(studentRequestService.CreateGradeLevelHandledInformation(request));
        }
    }
}