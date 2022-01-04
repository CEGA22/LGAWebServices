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

        // POST: UpdateStudentInformation
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/student/update_student_information")]
        public IHttpActionResult UpdateStudentPassword(StudentAccount request)
        {
            var newsandannouncements = new StudentAccount();
            return Ok(newsandannouncements.UpdateStudentPassword(request));
        }
    

        // GET: StudentAccountPassword             
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_password/{email}")]
        public IEnumerable<StudentAccount> GetStudentAccountPassword(string email)
        {
            var studentaccount = new StudentAccount();
            return studentaccount.GetStudentAccountPassword(email);
        }

        // GET: Student
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/student/get_accountOnly")]
        public IEnumerable<StudentAccount> GetStudentAccountOnly()
        {
            var studentAccount = new StudentAccount();
            return studentAccount.GetStudentAccountOnly();
        }


        //// GET: StudentAccountbyLastname
        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //[Route("api/lga/student/get_by_id/{lastname}")]
        //public IEnumerable<StudentAccount>GetStudentAccountById(string lastname)
        //{
        //    var studentAccount = new StudentAccount();
        //    return studentAccount.GetStudentAccountDetailsByLastname(lastname);
        //}

        //// GET: SutdentAccountbyGradeLevel
        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //[Route("api/lga/student/get_by_gradelevel/{gradelevel}")]
        //public IEnumerable<StudentAccount> GetStudentAccountByGradeLevel(string gradelevel)
        //{
        //    var studentAccount = new StudentAccount();
        //    return studentAccount.GetStudentAccountDetailsByGradeLevel(gradelevel);
        //}

        //// GET: SutdentAccountbyGradeLevelFilter
        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //[Route("api/lga/student/get_by_gradelevelFilter")]
        //public  IEnumerable<StudentAccount> GetStudentAccountByGradeLevelFilter()
        //{
        //    var studentAccount = new StudentAccount();
        //    return studentAccount.GetStudentAccountDetailsByGradeLevelFilter();
        //}

        //// GET: SutdentAccountbyGradeLevel
        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //[Route("api/lga/student/get_by_section/{section}")]
        //public IEnumerable<StudentAccount> GetStudentAccountBySection(string section)
        //{
        //    var studentAccount = new StudentAccount();
        //    return studentAccount.GetStudentAccountDetailsBySection(section);
        //}

        //// GET: SutdentAccountbyGradeLevelFilter
        //[AcceptVerbs("GET", "POST")]
        //[HttpGet]
        //[Route("api/lga/student/get_by_sectionFilter")]
        //public IEnumerable<StudentAccount> GetStudentAccountBySectionFilter()
        //{
        //    var studentAccount = new StudentAccount();
        //    return studentAccount.GetStudentAccountDetailsBySectionFilter();
        //}
    }
}