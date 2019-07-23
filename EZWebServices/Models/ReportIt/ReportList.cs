using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class ReportList
    {
        public string report_no { get; set; }
        public string user { get; set; }
        public string ip { get; set; }
        public string reported_by { get; set; }
        public string reported_by_email { get; set; }
        public string reported_by_phone { get; set; }
        public DateTime reported_on { get; set; }
        public string status { get; set; }

        public List<ReportListData> GetReportList(string user, string status)
        {

            var reportList = new List<ReportList>();

            user = user.Contains("@") ? user : user + "@kaec.net";

            status = status.ToLower() == "all" ? "" : " AND status='" + status +"'"; 

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_report WHERE user=@user" + status, con);
                cmd.Parameters.AddWithValue("@user", user);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                { 
                    reportList.Add(new ReportList
                    {
                        //id = int.Parse(dr["id"].ToString()),
                        ip = dr["ip"].ToString(),
                        reported_by = dr["reported_by"].ToString(),
                        reported_by_email = dr["reported_by_email"].ToString(),
                        reported_by_phone = dr["reported_by_phone"].ToString(),
                        reported_on = Convert.ToDateTime(dr["reported_on"].ToString()),
                        report_no = dr["report_no"].ToString(),
                        status = dr["status"].ToString(),
                        user = dr["user"].ToString()
                    });
                }
            }

            return new List<ReportListData>
            {
                new ReportListData
                {
                    data = reportList
                }
            };
        }

        public List<ReportList> GetMyReports(string user, string status)
        {

            var reportList = new List<ReportList>();

            user = user.Contains("@") ? user : user + "@kaec.net";

            status = status.ToLower() == "all" ? "" : " AND status='" + status + "'";

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_report WHERE user=@user" + status, con);
                cmd.Parameters.AddWithValue("@user", user);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    reportList.Add(new ReportList
                    {
                        //id = int.Parse(dr["id"].ToString()),
                        ip = dr["ip"].ToString(),
                        reported_by = dr["reported_by"].ToString(),
                        reported_by_email = dr["reported_by_email"].ToString(),
                        reported_by_phone = dr["reported_by_phone"].ToString(),
                        reported_on = Convert.ToDateTime(dr["reported_on"].ToString()),
                        report_no = dr["report_no"].ToString(),
                        status = dr["status"].ToString(),
                        user = dr["user"].ToString()
                    });
                }
            }

            return reportList;
        }

        public ReportList GetReportListDetails(string user, string report_no)
        {
            var objReturn = new ReportList();

            user = user.Contains("@") ? user : user + "@kaec.net";

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_report WHERE user=@user AND report_no=@report_no", con);
                cmd.Parameters.AddWithValue("@report_no", report_no);
                cmd.Parameters.AddWithValue("@user", user);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objReturn = new ReportList
                    {
                        //id = int.Parse(dr["id"].ToString()),
                        ip = dr["ip"].ToString(),
                        reported_by = dr["reported_by"].ToString(),
                        reported_by_email = dr["reported_by_email"].ToString(),
                        reported_by_phone = dr["reported_by_phone"].ToString(),
                        reported_on = Convert.ToDateTime(dr["reported_on"].ToString()),
                        report_no = dr["report_no"].ToString(),
                        status = dr["status"].ToString(),
                        user = dr["user"].ToString()
                    };
                }
            }

            return objReturn;
        }
    }

    public class ReportListData
    {
        public List<ReportList> data { get; set; }
    }
}