using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class StudentController : ApiController
    {

        // GET: Student
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_all")]
        public IEnumerable<StudentAccount> GetStudentAccount()
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountDetails();
        }

        // GET: StudentAccountbyLastname
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_by_id/{lastname}")]
        public IEnumerable<StudentAccount>GetStudentAccountById(string lastname)
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountDetailsByLastname(lastname);
        }

        // GET: SutdentAccountbyGradeLevel
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_by_gradelevel/{gradelevel}")]
        public IEnumerable<StudentAccount> GetStudentAccountByGradeLevel(string gradelevel)
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountDetailsByGradeLevel(gradelevel);
        }

        // GET: SutdentAccountbyGradeLevelFilter
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_by_gradelevelFilter")]
        public  IEnumerable<StudentAccount> GetStudentAccountByGradeLevelFilter()
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountDetailsByGradeLevelFilter();
        }

        // GET: SutdentAccountbyGradeLevel
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_by_section/{section}")]
        public IEnumerable<StudentAccount> GetStudentAccountBySection(string section)
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountDetailsBySection(section);
        }

        // GET: SutdentAccountbyGradeLevelFilter
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_by_sectionFilter")]
        public IEnumerable<StudentAccount> GetStudentAccountBySectionFilter()
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountDetailsBySectionFilter();
        }
    }
}