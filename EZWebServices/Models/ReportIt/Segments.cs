using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class Segments
    {
        public int id { get; set; }
        public int floor_id { get; set; }
        public string location_wing { get; set; }
        public string location_segment { get; set; }

        public List<SegmentsData> GetSegmentsData(string floor_id)
        {
            var listReturn = new List<Segments>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_segment WHERE floor_id='" + floor_id + "'", con);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new Segments
                    {
                        floor_id = int.Parse(dr["floor_id"].ToString()),
                        id = int.Parse(dr["id"].ToString()),
                        location_segment = dr["location_segment"].ToString(),
                        location_wing = dr["location_wing"].ToString(),
                    });
                }
            }

            var data = new List<SegmentsData>();
            data.Add(new SegmentsData
            {
                data = listReturn
            });

            return data;
        }

        public List<Segments> GetSegments(string floor_id)
        {
            var listReturn = new List<Segments>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_segment WHERE floor_id='" + floor_id + "'", con);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new Segments
                    {
                        floor_id = int.Parse(dr["floor_id"].ToString()),
                        id = int.Parse(dr["id"].ToString()),
                        location_segment = dr["location_segment"].ToString(),
                        location_wing = dr["location_wing"].ToString(),
                    });
                }
            }

            return listReturn;
        }
    }
}