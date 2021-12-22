using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace EZWebServices.Controllers
{
    public class SectionsHandledController : ApiController
    {
        // GET: SectionsHandled
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/lga/sectionsHandled/get_all/{ID}")]
        public IEnumerable<SectionsHandled> GetSectionsHandled(int ID)
        {
            var classRecord = new SectionsHandled();
            return classRecord.GetSectionsHandled(ID);
        }
    }
}