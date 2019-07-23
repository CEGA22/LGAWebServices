using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class StatusFilter
    {
        public int id { get; set; }
        public string status { get; set; }

        public List<StatusFilterData> GetStatusFilters()
        {
            var statusList = new List<StatusFilter>
            {
                new StatusFilter
                {
                    id = 1,
                    status = "All"
                },

                new StatusFilter
                {
                    id = 2,
                    status = "Draft"
                },

                new StatusFilter
                {
                    id = 3,
                    status = "New"
                },

                new StatusFilter
                {
                    id = 4,
                    status = "Ticketed"
                },

                new StatusFilter
                {
                    id = 5,
                    status = "Closed"
                }
            };

            return new List<StatusFilterData>
            {
                new StatusFilterData
                {
                    data = statusList
                }
            };
        }
    }

    public class StatusFilterData
    {
        public List<StatusFilter> data { get; set; }
    }

}