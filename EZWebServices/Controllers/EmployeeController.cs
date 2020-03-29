using EZWebServices.Models.Employees;
using System.Collections.Generic;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class EmployeeController : ApiController
    {
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/employee/search_on_directory/{keyWord}")]
        public List<EECEmployee> search_on_directory(string keyWord)
        {
            var eecEmployee = new EECEmployee();
            return eecEmployee.SearchOnDirectory(keyWord);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/employee/get_shares_line_manager/{lineManagerNo}")]
        public List<EECEmployee> get_shares_line_manager(string lineManagerNo)
        {
            var eecEmployee = new EECEmployee();
            return eecEmployee.GetSharesLineManager(lineManagerNo);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/employee/get_direct_reports/{empId}")]
        public List<EECEmployee> get_direct_reports(string empId)
        {
            var eecEmployee = new EECEmployee();
            return eecEmployee.GetDirectReports(empId);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/employee/manager_centre/{empId}")]
        public ManagerCentre manager_centre(string empId)
        {
            var managerCentre = new ManagerCentre();
            return managerCentre.GetManagerCentreDetails(empId);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/employee/nedap/{empId}")]
        public NEDAP get_nedap_access(string empId)
        {
            var nedap = new NEDAP();
            return nedap.GetNEDAPAccess(empId);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/employee/nedap_access/{empId}")]
        public NEDAP get_nedap_aeosdb_access(string empId)
        {
            var nedap = new NEDAP();
            return nedap.GetNEDAPAEOSDBAcccess(empId);
        }
    }
}