using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class GradeLevelController : ApiController
    {
        // GET: GradeLevel
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/gradelevel/get_all")]
        public IEnumerable<GradeLevelModel> GetGradeLevel()
        {
            var studentAccount = new GradeLevelModel();
            return studentAccount.GetGradeLevel();
        }
    }
}