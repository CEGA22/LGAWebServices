using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class Floors
    {
        public int floor_id { get; set; }
        public string floor { get; set; }
        public int floor_order { get; set; }

        public List<FloorsData> GetFloors()
        {
            var listReturn = new List<Floors>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_floor", con);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new Floors
                    {
                        floor = dr["floor"].ToString(),
                        floor_order = int.Parse(dr["floor_order"].ToString()),
                        floor_id = int.Parse(dr["id"].ToString()),
                    });
                }
            }

            var data = new List<FloorsData>();
            data.Add(new FloorsData
            {
                data = listReturn
            });

            return data;
        }

        public List<Floors> GetFloorsLocation()
        {
            var listReturn = new List<Floors>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_floor", con);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new Floors
                    {
                        floor = dr["floor"].ToString(),
                        floor_order = int.Parse(dr["floor_order"].ToString()),
                        floor_id = int.Parse(dr["id"].ToString()),
                    });
                }
            }

            return listReturn;
        }

    }
}