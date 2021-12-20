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
    public class SchoolRequestController : ApiController
    {
        // GET: SchoolRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/school/information")]
        public IHttpActionResult CreateSchoolAccountInformation(SchoolReuqest request)
        {
            var studentRequestService = new SchoolRequestService();
            return Ok(studentRequestService.CreateSchoolAccount(request));
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/school/delete_information_by_id/{ID}")]
        public IHttpActionResult DeleteSchoolAccountInformation(int ID)
        {
            var studentRequestService = new SchoolRequestService();
            return Ok(studentRequestService.DeleteSchoolAccount(ID));
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/school/update_information_by_id")]
        public IHttpActionResult UpdateSchoolAccountInformation(SchoolReuqest request)
        {
            var studentRequestService = new SchoolRequestService();
            return Ok(studentRequestService.UpdateSchoolAccount(request));
        }
    }
}