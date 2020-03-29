using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.Employees
{
    public class DirectReports
    {
        public string EmployeeeID { get; set; }
        public byte[] UserPhoto { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Position { get; set; }
        public string PositionArabic { get; set; }
        public string Grade { get; set; }
        public string HireDate { get; set; }
        public string Department { get; set; }
        public string CostCenter { get; set; }

    }
}