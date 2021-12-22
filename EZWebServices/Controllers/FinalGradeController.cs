using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace EZWebServices.Controllers
{
    public class FinalGradeController : ApiController
    {
        // GET: FinalGrade
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/finalgrade/get_all/{ID}")]
        public IEnumerable<FinalGrade> GetfinalGrades(int ID)
        {
            var classRecord = new FinalGrade();
            return classRecord.GetFinalGrades(ID);
        }
    }
}