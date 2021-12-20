using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class ClassScheduleController : ApiController
    {
        // GET: ClassSchedule
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_all")]
        public IEnumerable<ClassSchedule> GetClassSchedules()
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleDetails();
        }


        // GET: StudentRequest
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/classSchedule/information")]
        public IHttpActionResult CreateClassScheduleInformation(ClassScheduleRequest request)
        {
            var studentRequestService = new ClassScheduleRequestService();
            return Ok(studentRequestService.CreateClassScheduleRequest(request));
        }

        // GET: ClassScheduleByWeekDay
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_by_Weekday/{weekday}")]
        public IEnumerable<ClassSchedule> GetClassSchedulesByWeekday(string weekday)
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleByWeekdayDetails(weekday);
        }

        // GET: ClassScheduleFaculty
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_all_faculty/{ID}")]
        public IEnumerable<ClassSchedule> GetClassSchedulesFaculty(int ID)
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleDetailsFaculty(ID);
        }

        // GET: ClassScheduleByWeekDayFaculty
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_by_Weekday_Faculty/{weekday}")]
        public IEnumerable<ClassSchedule> GetClassSchedulesByWeekdayFaculty(string weekday)
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleByWeekdayDetailsFaculty(weekday);
        }

        // GET: ClassScheduleByWeekFaculty
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_by_Week_Faculty/{ID}/{weekday}")]
        public IEnumerable<ClassSchedule> GetClassSchedulesByWeekFaculty(int ID, string weekday)
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleByWeekDetailsFaculty(ID, weekday);
        }

        // GET: ClassScheduleStudent
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_all_student/{ID}")]
        public IEnumerable<ClassSchedule> GetClassSchedulesStudent(int ID)
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleStudent(ID);
        }

        // GET: ClassScheduleByWeekDayStudent
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/classSchedule/get_by_Weekday_Student/{ID}/{weekday}")]
        public IEnumerable<ClassSchedule> GetClassSchedulesByWeekdayStudent(int ID, string weekday)
        {
            var studentAccount = new ClassSchedule();
            return studentAccount.GetClassScheduleByWeekdayDetailsStudent(ID, weekday);
        }
    }
}