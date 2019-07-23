using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class SubmitReports
    {
        public string subject { get; set; }
        public string remarks { get; set; }

        public SubmitReports SubmitReport(string report_no)
        {
            var objReturn = new SubmitReports();
            int reportSuccess = 0, concernSuccess = 0;

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("UPDATE reportit_report SET status='New' WHERE report_no=@report_no", con);
                cmd.Parameters.AddWithValue("@report_no", report_no);
                reportSuccess = cmd.ExecuteNonQuery();
            }

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("UPDATE reportit_concern SET status='NEW' WHERE report_no=@report_no", con);
                cmd.Parameters.AddWithValue("@report_no", report_no);
                concernSuccess = cmd.ExecuteNonQuery();
            }

            if (reportSuccess > 0 && concernSuccess > 0)
            {
                objReturn = new SubmitReports
                {
                    remarks = "You successfully reported all your concerns!",
                    subject = "Reported"
                };
            }

            else
            {
                objReturn = new SubmitReports
                {
                    remarks = "Error on submission of concerns!",
                    subject = "Error"
                };
            }

            return objReturn;
        }

    }
}