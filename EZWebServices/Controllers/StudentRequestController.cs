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
    public class StudentRequestController : ApiController
    {
        // GET: StudentRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/student/information")]
        public IHttpActionResult CreateStudentInformation(StudentRequest request)
        {
            var studentRequestService = new StudentRequestService();
            return Ok(studentRequestService.CreateStudentInformation(request));
        }

        // GET: UpdateStudentRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/student/update_information")]
        public IHttpActionResult UpdateStudentInformation(StudentRequest request)
        {
            var studentRequestService = new StudentRequestService();
            return Ok(studentRequestService.UpdateStudentInformation(request));
        }
    }
}