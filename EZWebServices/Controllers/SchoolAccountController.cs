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
    }
}