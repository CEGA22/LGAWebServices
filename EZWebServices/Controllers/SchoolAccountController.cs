using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace EZWebServices.Controllers
{
    public class SchoolAccountController : ApiController
    {
        // GET: SchoolAccount
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/schoolAccount/get_all")]
        public IEnumerable<SchoolAccount> GetSchoolAccountDetails()
        {
            var studentAccount = new SchoolAccount();
            return studentAccount.GetSchoolAccountDetails();
        }

        // GET: Student
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/schoolAccount/get_accountOnly")]
        public IEnumerable<SchoolAccount> GetSchoolAccountOnly()
        {
            var schoolAccount = new SchoolAccount();
            return schoolAccount.GetStudentAccountOnly();
        }

        // GET: SchoolAccountPassword             
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/schoolAccount/get_password/{email}")]
        public IEnumerable<SchoolAccount> GetSchoolAccountPassword(string email)
        {
            var studentaccount = new SchoolAccount();
            return studentaccount.GetSchoolAccountPassword(email);
        }
    }
}