using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class GradeLevelSectionController : ApiController
    {
        // GET: GradeLevel
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/gradelevelsection/get_all")]
        public IEnumerable<GradeLevelSection> GetGradeLevel()
        {
            var studentAccount = new GradeLevelSection();
            return studentAccount.GetGradeLevel();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/gradelevelsection/information")]
        public IHttpActionResult CreateGradeLevelSectionInformation(GradeLevelSection request)
        {
            var studentRequestService = new GradeLevelSectionRequestService();
            return Ok(studentRequestService.CreateGradeLevelSectionInformation(request));
        }
    }
}