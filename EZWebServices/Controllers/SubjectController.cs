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

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/subjects/get_by_grade_level_id/{id}")]
        public IEnumerable<Subjects> GetSubjectsByGradeLevelId(int id)
        {
            var classRecord = new Subjects();
            return classRecord.GetSubjectsByGradeLevelId(id);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/subjects/get_subjects_handled/{teacherId}/{gradeLevelId}")]
        public IEnumerable<SubjectsHandled> GetSubjectsHandled(int teacherId, int gradeLevelId)
        {
            var subjects = new Subjects();
            return subjects.GetSubjectsHandled(teacherId, gradeLevelId);
        }
    }
}