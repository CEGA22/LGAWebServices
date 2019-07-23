using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.L8Commute
{
    public class OpenTrips
    {
        public int id { get; set; }
        public string trip_id { get; set; }
        public DateTime trip_date { get; set; }
        public string days_remaining { get; set; }
        public string random { get; set; }
        public int occupied_seats { get; set; }

        public List<OpenTripsData> GetOpenTrips()
        {
            var listReturn = new List<OpenTrips>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT id, trip_id, trip, random FROM seat WHERE gender = 'Driver' AND trip > '" + DateTime.Now + "'  ORDER BY trip_id ", con);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new OpenTrips
                    {
                        id = int.Parse(dr["id"].ToString()),
                        days_remaining = FormatTrip(Convert.ToDateTime(dr["trip"].ToString())),
                        random = dr["random"].ToString(),
                        occupied_seats = GetOccupiedSeats(dr["trip_id"].ToString()).occupied_seats,
                        trip_date = Convert.ToDateTime(dr["trip"].ToString()),
                        trip_id = dr["trip_id"].ToString()
                    });
                }
            }


            var data = new List<OpenTripsData>();
            data.Add(new OpenTripsData
            {
                data = listReturn
            });

            return data;
        }

        public OpenTrips GetOccupiedSeats(string tripId)
        {
            var objReturn = new OpenTrips();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT COUNT(seat) as seat FROM seat WHERE trip_id=@trip_id AND code='1'", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    objReturn = new OpenTrips
                    {
                        occupied_seats = int.Parse(dr["seat"].ToString())
                    };
                }
            }

            return objReturn;
        }

        private string FormatTrip(DateTime trip)
        {
            var span = (trip - DateTime.Now);
            return string.Format("{0}d, {1}h, {2}m",
                span.Days, span.Hours, span.Minutes);
        }
    }

}
