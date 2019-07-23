using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace EZWebServices.Models.Fleet
{
    public class FleetRequestAction
    {
        public string status { get; set; }
        public string remarks { get; set; }

        public FleetRequestAction FleetRequestApprove(string service_ref, string random, string randomize)
        {
            var fleetRequest = new FleetRequest();
            var requestDetails = fleetRequest.GetRequestsDetails(service_ref);

            if(requestDetails.service_ref != null)
            {
                if(requestDetails.random == random && requestDetails.randomize == randomize)
                {
                    //var request = WebRequest.Create("http://ez.kaec.net/seat/main/fleet_link?random=" + random + "&randomize=" + randomize + "&action=15A329C8") as HttpWebRequest;
                    var request = WebRequest.Create("http://mydirectory.kaec.net/eec/main/fleet_verify?random=" + random + "&randomize=" + randomize) as HttpWebRequest;
                    var response = request.GetResponse() as HttpWebResponse;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //try {
                        //    using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                        //    {

                        //        con.Open();
                        //        var cmd = new MySqlCommand();
                        //        cmd = new MySqlCommand("UPDATE fleet_request " +
                        //                                        "SET method='EEC Mobility' WHERE service_ref=@service_ref", con);
                        //        cmd.Parameters.AddWithValue("@service_ref", service_ref);
                        //        var result = cmd.ExecuteNonQuery();
                        //        if (result > 0)
                        //        {

                        return new FleetRequestAction
                        {
                            status = "success",
                            remarks = "approved by line manager",
                        };
                        //        }

                        //        else
                        //        {
                        //            return new FleetRequestAction
                        //            {
                        //                status = "failed",
                        //                remarks = "api error has occured",
                        //            };
                        //        }
                        //    }


                    }

                    //    catch
                    //    {
                    //        return new FleetRequestAction
                    //        {
                    //            status = "failed",
                    //            remarks = "api error has occured",
                    //        };
                    //    }
                    //}

                    else
                    {
                        return new FleetRequestAction
                        {
                            status = "failed",
                            remarks = "link error has occured",
                        };
                    }
                }

                else
                {
                    return new FleetRequestAction
                    {
                        status = "failed",
                        remarks = "action has already been taken",
                    };
                }
            }

            else
            {
                return new FleetRequestAction
                {
                    status = "failed",
                    remarks = "record not found",
                };
            }

           

        }

        public FleetRequestAction FleetRequestDisapprove(string service_ref, string random, string randomize)
        {
            var fleetRequest = new FleetRequest();
            var requestDetails = fleetRequest.GetRequestsDetails(service_ref);

            if (requestDetails.service_ref != null)
            {
                if (requestDetails.random == random && requestDetails.randomize == randomize)
                {
                    //var request = WebRequest.Create("http://ez.kaec.net/seat/main/fleet_link?random=" + random + "&randomize=" + randomize + "&action=3KL5398T") as HttpWebRequest;
                    var request = WebRequest.Create("http://mydirectory.kaec.net/eec/main/fleet_disapprove?random=" + random + "&randomize=" + randomize) as HttpWebRequest;
                    var response = request.GetResponse() as HttpWebResponse;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //try
                        //{
                        //    //using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                        //    //{

                        //    //    con.Open();
                        //    //    var cmd = new MySqlCommand();
                        //    //    cmd = new MySqlCommand("UPDATE fleet_request " +
                        //    //                                    "SET method='EEC Mobility' WHERE service_ref=@service_ref", con);
                        //    //    cmd.Parameters.AddWithValue("@service_ref", service_ref);
                        //    //    var result = cmd.ExecuteNonQuery();
                        //    //    if (result > 0)
                        //    //    {
                        return new FleetRequestAction
                        {
                            status = "success",
                            remarks = "not approved by line manager",
                        };
                        //    //    }

                        //    //    else
                        //    //    {
                        //    //        return new FleetRequestAction
                        //    //        {
                        //    //            status = "failed",
                        //    //            remarks = "api error has occured",
                        //    //        };
                        //    //    }
                        //    //}


                        //}

                        //catch
                        //{
                        //    return new FleetRequestAction
                        //    {
                        //        status = "failed",
                        //        remarks = "api error has occured",
                        //    };
                        //}
                    }

                    else
                    {
                        return new FleetRequestAction
                        {
                            status = "failed",
                            remarks = "link error has occured",
                        };
                    }
                }

                else
                {
                    return new FleetRequestAction
                    {
                        status = "failed",
                        remarks = "action has already been taken",
                    };
                }
            }

            else
            {
                return new FleetRequestAction
                {
                    status = "failed",
                    remarks = "record not found",
                };
            }



        }
    }
}