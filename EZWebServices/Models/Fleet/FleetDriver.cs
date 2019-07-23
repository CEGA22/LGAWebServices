using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.Fleet
{
    public class FleetDriver
    {
        public int id { get; set; }
        public string driver_id { get; set; }
        public string driver_name { get; set; }
        public string mobile { get; set; }
        public int code { get; set; }

        public List<FleetDriver> GetFleetDrivers()
        {
            var listReturn = new List<FleetDriver>();
            using (var con = new MySqlConnection(ConnectionHelper.BaseConnection()))
            {
                con.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM driver", con))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        listReturn.Add(new FleetDriver
                        {
                            id = int.Parse(dr["id"].ToString()),
                            driver_id = dr["driver_id"].ToString(),
                            driver_name = dr["name"].ToString(),
                            code = int.Parse(dr["code"].ToString()),
                            mobile = dr["mobile"].ToString(),
                        });
                    }

                    dr.Close();
                }
                con.Close();
            }

            return listReturn;
        }

        public List<FleetDriver> GetFleetDrivers(string driverId)
        {
            var listReturn = new List<FleetDriver>();
            using (var con = new MySqlConnection(ConnectionHelper.BaseConnection()))
            {
                con.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM driver WHERE driver_id=@driver_id", con))
                {
                    cmd.Parameters.AddWithValue("@driver_id", driverId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            listReturn.Add(new FleetDriver
                            {
                                id = int.Parse(dr["id"].ToString()),
                                driver_id = dr["driver_id"].ToString(),
                                driver_name = dr["name"].ToString(),
                                code = int.Parse(dr["code"].ToString()),
                                mobile = dr["mobile"].ToString()
                            });
                        }

                        dr.Close();
                    }
                    con.Close();
                }
            }

            return listReturn;
        }
    }
}