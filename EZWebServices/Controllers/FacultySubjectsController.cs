using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class FacultySubjectsController : ApiController
    {
        // GET: FacultySubjects
        // GET: ClassRecord             
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/facultySubjects/get_all/{ID}")]
        public IEnumerable<FacultySubjects> GetClassRecords(int ID)
        {
            var facultysubjects = new FacultySubjects();
            return facultysubjects.GetFacultySubjects(ID);
        }
    }
}