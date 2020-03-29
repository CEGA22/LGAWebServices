using EZWebServices.Helpers;
using EZWebServices.Models.Employees;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models.Fleet
{
    public class FleetNotification
    {
        public int Notifications { get; set; }
        public string Tag { get; set; }

        public FleetNotification GetRequestsForApprovals(string line_manager_id)
        {

            var notificationReturn = new FleetNotification();
            var ctr = 0;

            if (!line_manager_id.Contains("@"))
            {
                line_manager_id = line_manager_id + "@kaec.net";
            }
            var eecEmployees = new EECEmployee();
            line_manager_id = eecEmployees.GetEmployeeDetails(line_manager_id)[0].emp_ID;

            var tempList = new List<FleetRequest>();

            using (var con = new MySqlConnection(ConnectionHelper.MyDirectoryConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT * FROM fleet_request " +
                                                "WHERE code='0' AND randomize!='9' ORDER BY from_date DESC", con);
                cmd.Parameters.AddWithValue("@line_manager_id", line_manager_id);

                using (var dr = cmd.ExecuteReader())
                {

                    tempList = FleetRequest.PopulateFleetRequest(dr);

                    dr.Close();
                }

                con.Close();

            }


            foreach (var items in tempList)
            {
                var result = new List<EECEmployee>();
                result = eecEmployees.GetEmployeeDetails(items.emp_id);

                if (result.Count == 0)
                {
                    var nonEEC = new NonEECEmployee();
                    var nonEECEmployeeDetails = nonEEC.GetEmployeeDetails(items.emp_id);

                    result.Add(new EECEmployee
                    {
                        line_Manager_No = nonEECEmployeeDetails.manager_id
                    });
                }

                else
                {
                    result = eecEmployees.GetEmployeeDetails(items.emp_id);
                }

                if (result.Count != 0)
                {
                    foreach (var employeeDetails in result)
                    {
                        if (employeeDetails.line_Manager_No == line_manager_id)
                        {
                            ctr = ctr + 1;
                            break;
                        }
                    }
                }

            }

            notificationReturn = new FleetNotification
            {
                Notifications = ctr,
                Tag = "fleet approvals",
            };

            return notificationReturn;
        }
    }
}