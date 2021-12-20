using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class AboutController : ApiController
    {
        // GET: About      
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/about/get_all")]
        public IEnumerable<About> GetAbout()
        {
            var studentAccount = new About();
            return studentAccount.GetAboutDetails();
        }
    }
}