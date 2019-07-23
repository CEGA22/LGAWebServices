using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.L8Commute
{
    public class TripDetails
    {
        public string trip_id { get; set; }
        public DateTime trip_date { get; set; }
        public string driver { get; set; }
        public string phone { get; set; }
        public string car { get; set; }
        public string trip { get; set; }
        public List<Passengers> passengers { get; set; }

        public TripDetails GetTripDetails(string tripId)
        {
            var objReturn = new TripDetails();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT id, trip_id, trip, pax_name, gender, pax_email, pax_id, cast(seat as int) as seat, cost_center,ip, date_submit, cast(code as int) as code, random FROM seat WHERE trip_id=@trip_id AND gender='Driver'", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    objReturn = new TripDetails
                    {
                        car = dr["pax_id"].ToString(),
                        driver = dr["pax_name"].ToString(),
                        phone = dr["ip"].ToString(),
                        passengers = GetPassengers(tripId),
                        trip = dr["cost_center"].ToString(),
                        trip_date = Convert.ToDateTime(dr["trip"].ToString()),
                        trip_id = tripId,
                    };
                }
            }

            return objReturn;
        }

        private List<Passengers> GetPassengers(string tripId)
        {
            var listReturn = new List<Passengers>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT pax_name, pax_id, cast(seat as int) as seat FROM seat WHERE trip_id=@trip_id AND code='1'", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new Passengers
                    {
                        pax_id = dr["pax_id"].ToString(),
                        pax_name = dr["pax_name"].ToString(),
                        seat = int.Parse(dr["seat"].ToString())
                    });
                }
            }

            return listReturn;
        }
    }
}