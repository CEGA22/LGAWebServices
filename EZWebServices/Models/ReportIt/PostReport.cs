using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class PostReport
    {
        public string report_no { get; set; }
        public string user { get; set; }
        public string ip { get; set; }
        public string reported_by { get; set; }
        public string reported_by_email { get; set; }
        public string reported_by_phone { get; set; }
        public DateTime reported_on{ get; set; }
        public string status { get; set; }

        public PostReportRemarks CreateReport(PostReport report)
        {
            var objReturn = new PostReportRemarks();

            var reportNo = "R" + RandomizeHelper.GenerateRandomString(7);

            var draft = GetDraftReport(report.reported_by_email);
            if(draft.report_no != null)
            {
                reportNo = draft.report_no;

                objReturn = new PostReportRemarks
                {
                    report_no = reportNo,
                    remarks = "You have a pending report!",
                    subject = "Report"
                };
            }

            else
            {
                using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                {
                    con.Open();
                    var cmd = new MySqlCommand("INSERT INTO reportit_report (report_no, user, ip, reported_by, reported_by_email, reported_by_phone, reported_on, status) VALUES(@report_no, @user, @ip, @reported_by, @reported_by_email, @reported_by_phone, @reported_on, @status)", con);
                    cmd.Parameters.AddWithValue("@report_no", reportNo);
                    cmd.Parameters.AddWithValue("@user", report.reported_by_email);
                    cmd.Parameters.AddWithValue("@ip", "EEC Mobility");
                    cmd.Parameters.AddWithValue("@reported_by", report.reported_by);
                    cmd.Parameters.AddWithValue("@reported_by_email", report.reported_by_email);
                    cmd.Parameters.AddWithValue("@reported_by_phone", report.reported_by_phone);
                    cmd.Parameters.AddWithValue("@reported_on", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", "Draft");

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        objReturn = new PostReportRemarks
                        {
                            report_no = reportNo,
                            remarks = "Successfully created new report!",
                            subject = "Report"
                        };
                    }

                    else
                    {
                        objReturn = new PostReportRemarks
                        {
                            remarks = "Error on creation of new report!",
                            subject = "Error"
                        };
                    }
                }
            }
            

            return objReturn;
        }

        public PostReport GetDraftReport(string user)
        {
            var objReturn = new PostReport();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT report_no FROM reportit_report WHERE (user=@user OR reported_by_email=@user) AND status='Draft' LIMIT 1", con);
                cmd.Parameters.AddWithValue("@user", user);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    objReturn =new PostReport
                    {
                        report_no = dr["report_no"].ToString(),
                    };
                }
            }

            return objReturn;
        }


    }
}