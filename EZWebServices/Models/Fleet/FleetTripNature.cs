using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EZWebServices.Models.Fleet
{
    public class FleetTripNature
    {
        public int id { get; set; }
        public string nature { get; set; }

        public List<FleetTripNature> GetFleetTripNature()
        {
            var listReturn = new List<FleetTripNature>();
            using (var con = new MySqlConnection(ConnectionHelper.BaseConnection()))
            {
                con.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM fleet_trip_nature", con))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listReturn.Add(new FleetTripNature
                        {
                            id = int.Parse(dr["id"].ToString()),
                            nature = dr["nature"].ToString()
                        });
                    }
                        
                    dr.Close();
                }
                con.Close();
            }

            return listReturn;
        }

    }
}