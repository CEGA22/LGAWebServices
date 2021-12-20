using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class StudentBalanceController : ApiController
    {
        // GET: StudentBalance
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/studentBalance/get_all")]
        public IEnumerable<StudentBalance> GetStudentBalance()
        {
            var studentAccount = new StudentBalance();
            return studentAccount.GetStudentBalance();
        }


        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/studentBalance/get_by_id/{id}")]
        public IEnumerable<StudentBalance> GetStudentBalanceByAccount(int ID)
        {
            var studentAccount = new StudentBalance();
            return studentAccount.GetStudentBalanceByAccount(ID);
        }


    }
}