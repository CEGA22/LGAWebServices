using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class SectionsHandledRequest
    {
        public IEnumerable<SectionsHandled> SectionsHandled { get; set; }

        public int TeacherId { get; set; }
    }
}