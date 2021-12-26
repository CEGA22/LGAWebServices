using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class NewsAndAnnouncementsController : ApiController
    {
        // GET: NewsAndAnnouncements
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/newsAndAnnouncements/get_all")]
        public IEnumerable<NewsAndAnnouncements> GetNewsAndAnnouncements()
        {
            var newsandannouncements = new NewsAndAnnouncements();
            return newsandannouncements.GetNewsAndAnnouncements();
        }
        

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/newsAndAnnouncements/update_information")]
        public IHttpActionResult UpdateStudentInformation(NewsAndAnnouncements request)
        {
            var newsandannouncements = new NewsAndAnnouncements();
            return Ok(newsandannouncements.UpdateNewsAndAnnouncement(request));
        }
    }
}