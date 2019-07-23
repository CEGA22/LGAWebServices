using EZWebServices.Helpers;
using EZWebServices.Models.Employees;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.L8Commute
{
    public class PostRequest
    {
        public string trip_id { get; set; }
        public int seat_no { get; set; }
        public string emp_id { get; set; }
        public string random { get; set; }

        public RequestRemarks CreateUpdateRequest(PostRequest newRequest)
        {
            var objReturn = new RequestRemarks();

            var eecEmployee = new EECEmployee();
            var employeeInfo = eecEmployee.SearchOnDirectory(newRequest.emp_id);
            var info = employeeInfo.Count != 0 ? employeeInfo[0] : null;

            if (info != null)
            {

                if (IsTripValid(newRequest.trip_id))
                {

                    if (newRequest.seat_no >= 0 && newRequest.seat_no <= 6)
                    {

                        if (!IsSeatAvailable(newRequest.trip_id, newRequest.seat_no))
                        {
                            objReturn = new RequestRemarks
                            {
                                remarks = "Seat selected is no longer available!",
                                subject = "Already Taken"
                            };
                        }

                        else
                        {

                            if (IsUpdateSeat(info.emp_ID, newRequest.trip_id, newRequest.seat_no))
                            {

                                if (IsRandomCodeValid(newRequest.trip_id, newRequest.random))
                                {
                                    using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                                    {
                                        con.Open();
                                        var cmd = new MySqlCommand("UPDATE seat SET seat=@seat,date_submit=@date_submit WHERE pax_id=@pax_id AND trip_id=@trip_id", con);
                                        cmd.Parameters.AddWithValue("@seat", newRequest.seat_no);
                                        cmd.Parameters.AddWithValue("@date_submit", DateTime.Now);
                                        cmd.Parameters.AddWithValue("@trip_id", newRequest.trip_id);
                                        cmd.Parameters.AddWithValue("@pax_id", info.emp_ID);

                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            objReturn = new RequestRemarks
                                            {
                                                remarks = "You have updated your reserved seat!",
                                                subject = "Updated"
                                            };

                                            AppendLog(info.e_Mail.Split('@')[0], " updated to " + newRequest.seat_no + " on ", newRequest.trip_id);

                                        }
                                    }
                                }

                                else
                                {
                                    objReturn = new RequestRemarks
                                    {
                                        remarks = "Trip not found.",
                                        subject = "Notice"
                                    };
                                }
                            }

                            else
                            {

                                var tripDetails = new TripDetails();
                                var details = tripDetails.GetTripDetails(newRequest.trip_id);
                                using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                                {
                                    con.Open();
                                    var cmd = new MySqlCommand("INSERT INTO seat ( trip_id, trip, pax_name, gender, pax_email, pax_id, seat, cost_center, ip, date_submit, code, random) VALUES(@trip_id,@trip,@pax_name,@gender,@pax_email,@pax_id,@seat,@cost_center,@ip,@date_submit,@code,@random)", con);
                                    cmd.Parameters.AddWithValue("@trip_id", newRequest.trip_id);
                                    cmd.Parameters.AddWithValue("@trip", details.trip_date);
                                    cmd.Parameters.AddWithValue("@pax_name", info.employee_Name_English);
                                    cmd.Parameters.AddWithValue("@gender", info.gender);
                                    cmd.Parameters.AddWithValue("@pax_email", info.e_Mail);
                                    cmd.Parameters.AddWithValue("@pax_id", info.emp_ID);
                                    cmd.Parameters.AddWithValue("@seat", newRequest.seat_no);
                                    cmd.Parameters.AddWithValue("@cost_center", info.cost_Center);
                                    cmd.Parameters.AddWithValue("@ip", "EEC Mobility");
                                    cmd.Parameters.AddWithValue("@date_submit", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@code", 1);
                                    cmd.Parameters.AddWithValue("@random", RandomizeHelper.GenerateRandomString());

                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        objReturn = new RequestRemarks
                                        {
                                            remarks = "You have reserved a seat!",
                                            subject = "Reserved"
                                        };

                                        AppendLog(info.e_Mail.Split('@')[0] , " reserved " + newRequest.seat_no + " on ", newRequest.trip_id);
                                    }

                                }
                            }
                        }
                    }

                    else
                    {
                        objReturn = new RequestRemarks
                        {
                            remarks = "Seat not found.",
                            subject = "Notice"
                        };
                    }
                }

                else
                {
                    objReturn = new RequestRemarks
                    {
                        remarks = "Trip not found.",
                        subject = "Notice"
                    };
                }
            }

            else
            {
                objReturn = new RequestRemarks
                {
                    remarks = "Request should be initiated by an EEC Employee.",
                    subject = "Notice"
                };
            }

            return objReturn;
        }

        public RequestRemarks CancelRequest(PostRequest cancelRequest)
        {
            var objReturn = new RequestRemarks();

            if (IsRandomCodeValid(cancelRequest.trip_id, cancelRequest.random))
            {

                var eecEmployee = new EECEmployee();
                var employeeInfo = eecEmployee.SearchOnDirectory(cancelRequest.emp_id);
                var info = employeeInfo.Count != 0 ? employeeInfo[0] : null;

                using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                {
                    con.Open();
                    var cmd = new MySqlCommand("DELETE FROM seat WHERE random=@random AND trip_id=@trip_id", con);
                    cmd.Parameters.AddWithValue("@trip_id", cancelRequest.trip_id);
                    cmd.Parameters.AddWithValue("@random", cancelRequest.random);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        objReturn = new RequestRemarks
                        {
                            remarks = "You have cancelled your reserved seat!",
                            subject = "Cancelled"
                        };

                        AppendLog(info.e_Mail.Split('@')[0], " cancelled ", cancelRequest.trip_id + " reservation");

                    }
                }
            }

            else
            {
                objReturn = new RequestRemarks
                {
                    remarks = "Trip not found.",
                    subject = "Notice"
                };
            }

            return objReturn;
        }

        private bool IsSeatAvailable(string tripId, int seatNo)
        {
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM seat WHERE trip_id=@trip_id AND seat=@seat", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                cmd.Parameters.AddWithValue("@seat", seatNo);
                var dr = cmd.ExecuteReader();
                return dr.HasRows ? false : true;
            }
        }

        private bool IsTripValid(string tripId)
        {
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT trip_id FROM seat WHERE trip_id=@trip_id", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();
                return dr.HasRows ? true : false;
            }
        }

        private bool IsUpdateSeat(string paxId, string tripId, int seatNo)
        {
            var blnReturn = false;

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT  cast(seat as int) as seat FROM seat WHERE trip_id=@trip_id AND  pax_id='" + paxId + "'", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if(int.Parse(dr["seat"].ToString()) != seatNo)
                    {
                        blnReturn = true;
                    }

                    else
                    {
                        blnReturn = false;
                    }
                }
            }

            return blnReturn;
        }

        private bool IsRandomCodeValid(string tripId, string random)
        {
            var objReturn = new SeatArrangement();

            random = string.IsNullOrWhiteSpace(random) ? " " : random;

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT pax_id FROM seat WHERE trip_id=@trip_id AND random=@random", con);
                cmd.Parameters.AddWithValue("@trip_id", tripId);
                cmd.Parameters.AddWithValue("@random", random);
                var dr = cmd.ExecuteReader();
                return dr.HasRows ? true : false;
            }
        }

        private void AppendLog(string pax, string action, string tripId)
        {
            string path = @"C:\xampp\htdocs\seat\l8commute.log";
           
            using (var sw = File.AppendText(path))
            {
                sw.Write("\r\n" + pax);
                sw.Write(action);
                sw.Write(tripId);
                sw.Write(" (EEC Mobility " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ")");
            }
        }

    }
}