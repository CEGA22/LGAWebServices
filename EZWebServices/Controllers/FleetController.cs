using EZWebServices.Helpers;
using EZWebServices.Models.Fleet;
using System.Collections.Generic;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class FleetController : ApiController
    {

        [Route("api/ez/fleet/get_fleet_nature")]
        public List<FleetTripNature> get_fleet_nature()
        {
            var fleetTripNature = new FleetTripNature();
            return fleetTripNature.GetFleetTripNature();
        }

        [Route("api/ez/fleet/get_fleet_cars")]
        public List<FleetCars> get_fleet_cars()
        {
            var fleetCars = new FleetCars();
            return fleetCars.GetFleetCars();
        }

        [Route("api/ez/fleet/get_fleet_cars/{id}")]
        public List<FleetCars> get_fleet_cars(string id)
        {
            var fleetCars = new FleetCars();
            return fleetCars.GetFleetCars(id);
        }

        [Route("api/ez/fleet/get_fleet_drivers")]
        public List<FleetDriver> get_fleet_drivers()
        {
            var fleetDrivers = new FleetDriver();
            return fleetDrivers.GetFleetDrivers();
        }

        [Route("api/ez/fleet/get_fleet_drivers/{driver_id}")]
        public List<FleetDriver> get_fleet_drivers(string driver_id)
        {
            var fleetDrivers = new FleetDriver();
            return fleetDrivers.GetFleetDrivers(driver_id);
        }

        [Route("api/ez/fleet/get_fleet_requests/{level}/{emp_id}/{status}")]
        public List<FleetRequest> get_fleet_requests(string level, string emp_id, string status)
        {
            var fleetRequest = new FleetRequest();
            return fleetRequest.GetFleetRequests(level, emp_id, status);
        }

        [Route("api/ez/fleet/get_approvals_data/{line_manager_id}")]
        public List<FleetRequestData> get_approvals_data(string line_manager_id)
        {
            var fleetRequest = new FleetRequest();
            return fleetRequest.GetApprovalsData(line_manager_id);
        }

        [Route("api/ez/fleet/get_approval_count/{line_manager_id}")]
        public FleetNotification get_approval_count(string line_manager_id)
        {
            var fleetNotification = new FleetNotification();
            return fleetNotification.GetRequestsForApprovals(line_manager_id);
        }

        [Route("api/ez/fleet/get_for_approvals/{line_manager_id}")]
        public List<FleetRequest> get_for_approvals(string line_manager_id)
        {
            var fleetRequest = new FleetRequest();
            return fleetRequest.GetRequestsForApprovals(line_manager_id);
        }

        [Route("api/ez/fleet/get_request_details/{service_ref}")]
        public FleetRequest get_request_details(string service_ref)
        {
            var fleetRequest = new FleetRequest();
            return fleetRequest.GetRequestsDetails(service_ref);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/fleet/approve_request/{service_ref}/{random}/{randomize}")]
        public FleetRequestAction approve_request(string service_ref,string random, string randomize)
        {
            var fleetRequestAction = new FleetRequestAction();
            return fleetRequestAction.FleetRequestApprove(service_ref, random, randomize);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/ez/fleet/disapprove_request/{service_ref}/{random}/{randomize}")]
        public FleetRequestAction disapprove_request(string service_ref, string random, string randomize)
        {
            var fleetRequestAction = new FleetRequestAction();
            return fleetRequestAction.FleetRequestDisapprove(service_ref, random, randomize);
        }
    }
}