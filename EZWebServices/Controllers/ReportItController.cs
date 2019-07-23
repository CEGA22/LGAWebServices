
using EZWebServices.Models.ReportIt;
using System.Collections.Generic;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class ReportItController : ApiController
    {
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_floors")]
        public List<FloorsData> get_floors()
        {
            var floors = new Floors();
            return floors.GetFloors();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_floor_locations")]
        public List<Floors> get_floor_location()
        {
            var floors = new Floors();
            return floors.GetFloorsLocation();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_segments_data/{floor_id}")]
        public List<SegmentsData> get_segments_data(string floor_id)
        {
            var segments = new Segments();
            return segments.GetSegmentsData(floor_id);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_segments/{floor_id}")]
        public List<Segments> get_segments(string floor_id)
        {
            var segments = new Segments();
            return segments.GetSegments(floor_id);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/check_drafts/{user}")]
        public PostReport get_draft_report(string user)
        {
            var drafts = new PostReport();
            return drafts.GetDraftReport(user);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/ez/report_it/create_report")]
        public IHttpActionResult CreateReport(PostReport report)
        {
            var postReport = new PostReport();
            return Ok(postReport.CreateReport(report));
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/ez/report_it/create_concern")]
        public IHttpActionResult CreateConcern(PostConcern concern)
        {
            var postConcern = new PostConcern();
            return Ok(postConcern.SaveConcern(concern));
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/submit_reports/{report_no}")]
        public SubmitReports submit_reports(string report_no)
        {
            var submit = new SubmitReports();
            return submit.SubmitReport(report_no);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_status_filter")]
        public List<StatusFilterData> get_status_filter()
        {
            var statusFilter = new StatusFilter();
            return statusFilter.GetStatusFilters();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_reports/{user}/{status}")]
        public List<ReportListData> get_reports(string user, string status)
        {
            var reports = new ReportList();
            return reports.GetReportList(user,status);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_my_reports/{user}/{status}")]
        public List<ReportList> get_my_reports(string user, string status)
        {
            var reports = new ReportList();
            return reports.GetMyReports(user, status);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_report_details/{user}/{report_no}")]
        public ReportList get_report_details(string user, string report_no)
        {
            var reports = new ReportList();
            return reports.GetReportListDetails(user, report_no);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_concerns/{report_no}")]
        public List<ConcernListData> get_concerns(string report_no)
        {
            var concerns = new ConcernList();
            return concerns.GetConcernList(report_no);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_my_concerns/{report_no}")]
        public List<ConcernList> get_my_concerns(string report_no)
        {
            var concerns = new ConcernList();
            return concerns.GetMyConcerns(report_no);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/report_it/get_concern_details/{report_no}/{concern_no}")]
        public ConcernList get_concern_details(string report_no, string concern_no)
        {
            var concerns = new ConcernList();
            return concerns.GetConcernDetails(report_no, concern_no);
        }
    }
}