using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class SubjectController : ApiController
    {
        // GET: Subject                 
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/subjects/get_all")]
        public IEnumerable<Subjects> GetSubjects()
        {
            var classRecord = new Subjects();
            return classRecord.GetSubjects();
        }
    }
}