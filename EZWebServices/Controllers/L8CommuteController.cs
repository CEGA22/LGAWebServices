using EZWebServices.Models.L8Commute;
using System.Collections.Generic;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class L8CommuteController : ApiController
    {
        [Route("api/ez/late_commute/get_open_trips")]
        public List<OpenTripsData> get_open_trips()
        {
            var openTrips = new OpenTrips();
            return openTrips.GetOpenTrips();
        }

        [Route("api/ez/late_commute/get_seat_arrangement/{pax_id}/{trip_id}")]
        public List<SeatArrangementData> get_seat_arrangement(string pax_id, string trip_id)
        {
            var seatArrangement = new SeatArrangement();
            return seatArrangement.GetSeatArrangement(pax_id, trip_id);
        }

        [Route("api/ez/late_commute/get_trip_details/{trip_id}")]
        public TripDetails get_trip_details(string trip_id)
        {
            var tripDetails = new TripDetails();
            return tripDetails.GetTripDetails(trip_id);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/ez/late_commute/post_request")]
        public IHttpActionResult NewRequest(PostRequest request)
        {
            var postRequest = new PostRequest();
            return Ok(postRequest.CreateUpdateRequest(request));
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/ez/late_commute/cancel_request")]
        public IHttpActionResult CancelRequest(PostRequest request)
        {
            var postRequest = new PostRequest();
            return Ok(postRequest.CancelRequest(request));
        }
    }
}