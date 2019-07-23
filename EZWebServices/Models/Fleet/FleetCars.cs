using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.Fleet
{
    public class FleetCars
    {
        public int id { get; set; }
        public string plate { get; set; }
        public string model { get; set; }
        public int code { get; set; }
        public DateTime deliver { get; set; }
        public string owner { get; set; }
        public int rate_one { get; set; }
        public int rate_two { get; set; }
        public string remarks { get; set; }
        public string car { get; set; }

        public List<FleetCars> GetFleetCars()
        {
            var listReturn = new List<FleetCars>();
            using (var con = new MySqlConnection(ConnectionHelper.BaseConnection()))
            {
                con.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM fleet", con))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var deliverDate = dr["deliver"].ToString();

                        listReturn.Add(new FleetCars
                        {
                            id = int.Parse(dr["id"].ToString()),
                            car = dr["car"].ToString(),
                            code = int.Parse(dr["code"].ToString()),
                            deliver = DateTimeHelper.ValidateDateTime(deliverDate),
                            model = dr["model"].ToString(),
                            owner = dr["owner"].ToString(),
                            plate = dr["plate"].ToString(),
                            rate_one = int.Parse(dr["rate_one"].ToString()),
                            rate_two = int.Parse(dr["rate_two"].ToString()),
                            remarks = dr["remarks"].ToString(),
                        });
                    }

                    dr.Close();
                }
                con.Close();
            }

            return listReturn;
        }

        public List<FleetCars> GetFleetCars(string id)
        {
            var listReturn = new List<FleetCars>();
            using (var con = new MySqlConnection(ConnectionHelper.BaseConnection()))
            {
                con.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM fleet WHERE id=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var deliverDate = dr["deliver"].ToString();

                            listReturn.Add(new FleetCars
                            {
                                id = int.Parse(dr["id"].ToString()),
                                car = dr["car"].ToString(),
                                code = int.Parse(dr["code"].ToString()),
                                deliver = DateTimeHelper.ValidateDateTime(deliverDate),
                                model = dr["model"].ToString(),
                                owner = dr["owner"].ToString(),
                                plate = dr["plate"].ToString(),
                                rate_one = int.Parse(dr["rate_one"].ToString()),
                                rate_two = int.Parse(dr["rate_two"].ToString()),
                                remarks = dr["remarks"].ToString(),
                            });
                        }

                        dr.Close();
                    }
                }
                con.Close();
            }

            return listReturn;
        }
    }
}