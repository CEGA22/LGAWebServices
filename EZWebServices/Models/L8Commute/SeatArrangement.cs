using EZWebServices.Helpers;
using EZWebServices.Models.Employees;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.L8Commute
{
    public class SeatArrangement
    {
        public int id { get; set; }
        public string trip_id { get; set; }
        public DateTime trip { get; set; }
        public string pax_name { get; set; }
        public string gender { get; set; }
        public string pax_email { get; set; }
        public string pax_id { get; set; }
        public string phone { get; set; }
        public int seat_no { get; set; }
        public string cost_center { get; set; }
        public DateTime reserved_on { get; set; }
        public int code { get; set; }
        public string random { get; set; }
        public string type { get; set; }
        public string type_image_location { get; set; }

        public List<SeatArrangementData> GetSeatArrangement(string paxId, string tripId)
        {

            var employeeInfo = new EECEmployee();

            var listReturn = new List<SeatArrangement>();
            var listSeat = new List<int>();
            var tripDate = DateTime.Now;

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT id, trip_id, trip, pax_name, gender, pax_email, pax_id, cast(seat as int) as seat, cost_center,ip, date_submit, cast(code as int) as code, random FROM seat WHERE trip_id=@trip_id", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();


                while (dr.Read())
                {

                    var type = string.Empty;
                    var type_image_location = string.Empty;

                    if (dr["gender"].ToString() == "Driver")
                    {
                        type = "Driver";
                        type_image_location = "http://ez.kaec.net/assets/images/transport/chauffeur.png";
                    }

                    else
                    {
                        if(dr["pax_id"].ToString() == paxId)
                        {
                            type = "you";
                            type_image_location = "http://ez.kaec.net/assets/images/transport/you.png";
                        }

                        else if (dr["pax_email"].ToString().ToLower().Contains(paxId.ToLower()))
                        {
                            type = "you";
                            type_image_location = "http://ez.kaec.net/assets/images/transport/you.png";
                        }

                        else
                        {
                            type = "pax";
                            type_image_location = "http://ez.kaec.net/assets/images/transport/pax.png";
                        }
                    }

                    tripDate = Convert.ToDateTime(dr["trip"].ToString());

                    listReturn.Add(new SeatArrangement
                    {
                        id = int.Parse(dr["id"].ToString()),
                        code = int.Parse(dr["code"].ToString()),
                        cost_center = dr["cost_center"].ToString(),
                        reserved_on = Convert.ToDateTime(dr["date_submit"].ToString()),
                        gender = dr["gender"].ToString(),
                        pax_email = dr["pax_email"].ToString(),
                        pax_id = dr["pax_id"].ToString(),
                        pax_name = dr["pax_name"].ToString(),
                        phone = type == "Driver" ? dr["ip"].ToString() : employeeInfo.GetMobile(dr["pax_id"].ToString()).mobile,
                        random = dr["random"].ToString(),
                        seat_no = int.Parse(dr["seat"].ToString()),
                        trip = tripDate,
                        trip_id = dr["trip_id"].ToString(),
                        type = type,
                        type_image_location = type_image_location
                    });

                    listSeat.Add(seat_no = int.Parse(dr["code"].ToString()));

                }
            }

            if (listReturn.Count != 0)
            {

                for (var seat = 0; seat < 7; seat++)
                {
                    if (!listSeat.Any(o => o == seat))
                    {
                        listReturn.Add(new SeatArrangement
                        {
                            seat_no = seat,
                            trip = tripDate,
                            trip_id = tripId,
                            type = "seat",
                            type_image_location = "http://ez.kaec.net/assets/images/transport/seat.png",
                        });
                    }
                }
            }

            var data = new List<SeatArrangementData>();
            data.Add(new SeatArrangementData
            {
                data = listReturn.OrderBy(o => o.seat_no).ToList()
            });

            return data;

        }

        public List<SeatArrangement> GetOccupiedSeats(string paxId, string tripId)
        {

            var employeeInfo = new EECEmployee();

            var listReturn = new List<SeatArrangement>();
            var listSeat = new List<int>();
            var tripDate = DateTime.Now;

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT id, trip_id, trip, pax_name, gender, pax_email, pax_id, cast(seat as int) as seat, cost_center,ip, date_submit, cast(code as int) as code, random FROM seat WHERE trip_id=@trip_id", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();


                while (dr.Read())
                {

                    var type = string.Empty;
                    var type_image_location = string.Empty;

                    if (dr["gender"].ToString() == "Driver")
                    {
                        type = "Driver";
                        type_image_location = "http://ez.kaec.net/assets/images/transport/chauffeur.png";
                    }

                    else
                    {
                        if (dr["pax_id"].ToString() == paxId)
                        {
                            type = "you";
                            type_image_location = "http://ez.kaec.net/assets/images/transport/you.png";
                        }

                        else if (dr["pax_email"].ToString().ToLower().Contains(paxId.ToLower()))
                        {
                            type = "you";
                            type_image_location = "http://ez.kaec.net/assets/images/transport/you.png";
                        }

                        else
                        {
                            type = "pax";
                            type_image_location = "http://ez.kaec.net/assets/images/transport/pax.png";
                        }
                    }

                    tripDate = Convert.ToDateTime(dr["trip"].ToString());

                    listReturn.Add(new SeatArrangement
                    {
                        id = int.Parse(dr["id"].ToString()),
                        code = int.Parse(dr["code"].ToString()),
                        cost_center = dr["cost_center"].ToString(),
                        reserved_on = Convert.ToDateTime(dr["date_submit"].ToString()),
                        gender = dr["gender"].ToString(),
                        pax_email = dr["pax_email"].ToString(),
                        pax_id = dr["pax_id"].ToString(),
                        pax_name = dr["pax_name"].ToString(),
                        phone = type == "Driver" ? dr["ip"].ToString() : employeeInfo.GetMobile(dr["pax_id"].ToString()).mobile,
                        random = dr["random"].ToString(),
                        seat_no = int.Parse(dr["code"].ToString()),
                        trip = tripDate,
                        trip_id = dr["trip_id"].ToString(),
                        type = type,
                        type_image_location = type_image_location
                    });

                    listSeat.Add(seat_no = int.Parse(dr["code"].ToString()));

                }
            }

            

           
            return listReturn;

        }




    }
}