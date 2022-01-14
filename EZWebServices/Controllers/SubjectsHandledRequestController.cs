using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class SubjectsHandledRequestController : ApiController
    {
        // GET: SubjectHandledRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/subjects/subjectsHandled/")]
        public IHttpActionResult CreateSubjectsHandledInformation(SubjectsHandledRequest request)
        {
            var studentRequestService = new SubjectsHandledRequestService();
            return Ok(studentRequestService.CreateSubjectsHandledInformation(request));
        }     
    }
}