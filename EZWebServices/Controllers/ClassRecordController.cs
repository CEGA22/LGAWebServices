using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class ClassRecordController : ApiController
    {
        // GET: ClassRecord             
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classrecord/get_all/{ID}")]
        public IEnumerable<ClassRecord> GetClassRecords( int ID)
        {
            var classRecord = new ClassRecord();
            return classRecord.GetClassRecordsDetails(ID);
        }

        // GET: ClassRecordStudent             
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classrecordstudent/get_all/{ID}")]
        public IEnumerable<ClassRecord> GetClassRecordsStudent(int ID)
        {
            var classRecord = new ClassRecord();
            return classRecord.GetClassRecordsDetailsStudent(ID);
        }

        // GET: UpdateClassRecordRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/classrecord/update_grades")]
        public IHttpActionResult UpdateStudentInformation(ClassRecordRequest request)
        {
            var studentRequestService = new ClassRecordRequestService();
            return Ok(studentRequestService.UpdateClassRecord(request));
        }

        // GET: UpdateClassRecordRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/classrecord/update_finalgrades")]
        public IHttpActionResult UpdateFinalGrade(ClassRecordRequest request)
        {
            var studentRequestService = new ClassRecordRequestService();
            return Ok(studentRequestService.UpdateFinalGrade(request));
        }


    }
}