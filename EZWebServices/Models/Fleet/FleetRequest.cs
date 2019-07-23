using EZWebServices.Helpers;
using EZWebServices.Models.Employees;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace EZWebServices.Models.Fleet
{
    public class FleetRequest
    {
        public int id { get; set; }
        public string service_ref { get; set; } = "";
        public string emp_id { get; set; } = "";
        public string contact { get; set; } = "";
        public string email { get; set; } = "";
        public string name { get; set; } = "";
        public string cost_center { get; set; } = "";
        public string department { get; set; } = "";
        public int grade { get; set; }
        public string driver_id { get; set; } = "";
        public string pax { get; set; } = "";
        public string nature { get; set; } = "";
        public string pick { get; set; } = "";
        public string specific_pick { get; set; } = "";
        public string drop { get; set; } = "";
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string type { get; set; } = "";
        public string justification { get; set; } = "";
        public string remarks { get; set; } = "";
        public string share { get; set; } = "";
        public string random { get; set; } = "";
        public string randomize { get; set; } = "";
        public string ip { get; set; } = "";
        public string session { get; set; } = "";
        public string code { get; set; } = "";
        public string status { get; set; } = "";
        public string line_manager_id { get; set; } = "";
        public string line_manager { get; set; } = "";
        public string line_manager_email { get; set; } = "";
        public string timestamp { get; set; } = "";
        public string time { get; set; } = "";
        public string realtime { get; set; } = "";
        public string plate { get; set; } = "";
        public string driver { get; set; } = "";
        public string driver_mobile { get; set; } = "";
        public DateTime real_from_date { get; set; }
        public int start_km { get; set; }
        public DateTime real_to_date { get; set; }
        public int end_km { get; set; }
        public int sar { get; set; }
        public string complaint { get; set; } = "";
        public string note { get; set; } = "";
        public int rating { get; set; }

        public List<FleetRequest> GetFleetRequests(string level, string emp_id, string status)
        {
            var listReturn = new List<FleetRequest>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand();

                switch (level)
                {
                    case "manager":
                        cmd = new MySqlCommand("SELECT * FROM fleet_request " +
                                                "WHERE line_manager_id=@line_manager_id AND status LIKE '%" + status + "%' " +
                                                "ORDER BY timestamp DESC", con);
                        cmd.Parameters.AddWithValue("@line_manager_id", emp_id);
                        break;

                    case "staff":
                        cmd = new MySqlCommand("SELECT * FROM fleet_request " +
                                                "WHERE emp_id=@emp_id AND status LIKE '%" + status + "%' " +
                                                "ORDER BY timestamp DESC", con);
                        cmd.Parameters.AddWithValue("@emp_id", emp_id);
                        break;
                }



                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        listReturn.Add(new FleetRequest
                        {
                            code = dr["code"].ToString(),
                            //complaint = dr["complaint"].ToString(),
                            contact = dr["contact"].ToString(),
                            //cost_center = dr["cost_center"].ToString(),
                            //department = dr["department"].ToString(),
                            //driver = dr["driver"].ToString(),
                            driver_id = dr["driver_id"].ToString(),
                            //driver_mobile = dr["driver_mobile"].ToString(),
                            drop = dr["drop"].ToString(),
                            //email = dr["email"].ToString(),
                            emp_id = dr["emp_id"].ToString(),
                            //end_km = int.Parse(dr["end_km"].ToString()),
                            from_date = Convert.ToDateTime(dr["from_date"].ToString()),
                            grade = int.Parse(dr["grade"].ToString()),
                            id = int.Parse(dr["id"].ToString()),
                            ip = dr["ip"].ToString(),
                            justification = dr["justification"].ToString(),
                            line_manager = dr["line_manager"].ToString(),
                            line_manager_email = dr["line_manager_email"].ToString(),
                            line_manager_id = dr["line_manager_id"].ToString(),
                            name = dr["name"].ToString(),
                            nature = dr["nature"].ToString(),
                            //note = dr["note"].ToString(),
                            pax = dr["pax"].ToString(),
                            pick = dr["pick"].ToString(),
                            //plate = dr["plate"].ToString(),
                            random = dr["random"].ToString(),
                            randomize = dr["randomize"].ToString(),
                            //rating = int.Parse(dr["rating"].ToString()),
                            //realtime = dr["realtime"].ToString(),
                            //real_from_date = Convert.ToDateTime(dr["real_from_date"].ToString()),
                            //real_to_date = Convert.ToDateTime(dr["real_to_date"].ToString()),
                            remarks = dr["remarks"].ToString(),
                            //sar = int.Parse(dr["sar"].ToString()),
                            service_ref = dr["service_ref"].ToString(),
                            session = dr["session"].ToString(),
                            //share = dr["share"].ToString(),
                            specific_pick = dr["specific_pick"].ToString(),
                            //start_km = int.Parse(dr["start_km"].ToString()),
                            //status = dr["status"].ToString(),
                            //time = dr["time"].ToString(),
                            timestamp = dr["timestamp"].ToString(),
                            to_date = Convert.ToDateTime(dr["to_date"].ToString()),
                            type = dr["type"].ToString(),

                        });
                    }

                    dr.Close();
                }

                con.Close();
            }

            return listReturn;
        }

        public FleetRequest GetRequestsDetails(string service_ref)
        {
            var objReturn = new FleetRequest();
            var eecEmployees = new EECEmployee();
            using (var con = new MySqlConnection(ConnectionHelper.MyDirectoryConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT * FROM fleet_request " +
                                                "WHERE service_ref=@service_ref", con);
                cmd.Parameters.AddWithValue("@service_ref", service_ref);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    var fleetServeDetails = GetFleetServeDetails(dr["service_ref"].ToString());

                    var result = new List<EECEmployee>();

                    result = eecEmployees.GetEmployeeDetails(dr["emp_id"].ToString());

                    if (result.Count == 0)
                    {
                        var nonEEC = new NonEECEmployee();
                        var nonEECEmployeeDetails = nonEEC.GetEmployeeDetails(dr["emp_id"].ToString());

                        result.Add(new EECEmployee
                        {
                            cost_Center = nonEECEmployeeDetails.cost_center,
                            department = nonEECEmployeeDetails.department,
                            e_Mail = nonEECEmployeeDetails.email,
                            line_Manager_Name = nonEECEmployeeDetails.manager,
                            line_Manager_No = nonEECEmployeeDetails.manager_id
                        });
                    }


                    else
                    {
                        result = eecEmployees.GetEmployeeDetails(dr["emp_id"].ToString());
                    }

                    if (result.Count != 0)
                    {
                        foreach (var employeeDetails in result)
                        {
                            //if (employeeDetails.line_Manager_No == line_manager_id)
                            //{
                            var driverName = string.Empty;
                            var driverMobile = string.Empty;

                            var driverDetails = eecEmployees.GetEmployeeDetails(dr["driver_id"].ToString());

                            foreach (var details in driverDetails)
                            {
                                driverName = dr["nature"].ToString().ToLower().Contains("chauffeur") ? "WPS Driver" : details.employee_Name_English;
                                driverMobile = details.mobile;
                            }

                            objReturn = new FleetRequest
                            {

                                cost_center = employeeDetails.cost_Center,
                                department = employeeDetails.department,
                                driver = driverName,
                                driver_mobile = driverMobile,
                                email = employeeDetails.e_Mail,
                                line_manager = employeeDetails.line_Manager_Name,
                                line_manager_email = employeeDetails.line_Manager_Email,
                                line_manager_id = employeeDetails.line_Manager_No,

                                code = dr["code"].ToString(),
                                contact = dr["contact"].ToString(),

                                driver_id = dr["driver_id"].ToString(),

                                drop = dr["drop"].ToString(),

                                emp_id = dr["emp_id"].ToString(),

                                from_date = Convert.ToDateTime(dr["from_date"].ToString()),
                                grade = int.Parse(dr["grade"].ToString()),
                                id = int.Parse(dr["id"].ToString()),
                                ip = dr["ip"].ToString(),
                                justification = dr["justification"].ToString(),

                                name = dr["name"].ToString(),
                                nature = dr["nature"].ToString(),

                                pax = dr["pax"].ToString(),
                                pick = dr["pick"].ToString(),

                                random = dr["random"].ToString(),
                                randomize = dr["randomize"].ToString(),

                                remarks = dr["remarks"].ToString(),

                                service_ref = dr["service_ref"].ToString(),
                                session = dr["session"].ToString(),

                                specific_pick = dr["specific_pick"].ToString(),

                                timestamp = dr["timestamp"].ToString(),
                                to_date = nature.ToLower().Contains("chauffeur") ? Convert.ToDateTime(dr["from_date"].ToString()) : Convert.ToDateTime(dr["to_date"].ToString()),
                                type = dr["type"].ToString(),

                                complaint = fleetServeDetails.complaint,
                                end_km = fleetServeDetails.end_km,
                                note = fleetServeDetails.note,
                                plate = fleetServeDetails.plate,
                                sar = fleetServeDetails.sar,
                                
                            
                            };

                            break;
                            //}
                        }
                    }


                }
            }

            //var request = new TemplateRequest();
            ////request.registration_ids = listReturn.ToArray();
            //request.data = new TemplateRequest.Data(listReturn);

            //string json = new JavaScriptSerializer().Serialize(request);

            return objReturn;
        }

        public List<FleetRequest> GetRequestsForApprovals(string line_manager_id)
        {

            if (!line_manager_id.Contains(""))
            {
                line_manager_id = line_manager_id + "kaec.net";
            }

            var listReturn = new List<FleetRequest>();
            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT * FROM fleet_request " +
                                                "WHERE line_manager_email=@line_manager_id AND status='0 - pending'", con);
                cmd.Parameters.AddWithValue("@line_manager_id", line_manager_id);

                using (var dr = cmd.ExecuteReader())
                {

                    listReturn = PopulateFleetRequest(dr);

                    dr.Close();
                }

                con.Close();

            }

            //var request = new TemplateRequest();
            ////request.registration_ids = listReturn.ToArray();
            //request.data = new TemplateRequest.Data(listReturn);

            //string json = new JavaScriptSerializer().Serialize(request);

            return listReturn;
        }

        public List<FleetRequestData> GetApprovalsData(string line_manager_id)
        {

            var eecEmployees = new EECEmployee();


            if (!line_manager_id.Contains("@"))
            {
                line_manager_id = line_manager_id + "@kaec.net";
            }

            line_manager_id = eecEmployees.GetEmployeeDetails(line_manager_id)[0].emp_ID;

            var listReturn = new List<FleetRequest>();
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

                    tempList = PopulateFleetRequest(dr);

                    dr.Close();
                }

                con.Close();

            }


            foreach (var items in tempList)
            {
                var result = new List<EECEmployee>();
                result = eecEmployees.GetEmployeeDetails(items.emp_id);

                var fleetServeDetails = GetFleetServeDetails(items.service_ref);

                if (result.Count == 0)
                {
                    var nonEEC = new NonEECEmployee();
                    var nonEECEmployeeDetails = nonEEC.GetEmployeeDetails(items.emp_id);

                    result.Add(new EECEmployee
                    {
                        cost_Center = nonEECEmployeeDetails.cost_center,
                        department = nonEECEmployeeDetails.department,
                        e_Mail = nonEECEmployeeDetails.email,
                        line_Manager_Name = nonEECEmployeeDetails.manager,
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
                            var driverName = string.Empty;
                            var driverMobile = string.Empty;

                            var driverDetails = eecEmployees.GetEmployeeDetails(items.driver_id);

                            foreach (var details in driverDetails)
                            {
                                driverName = items.nature.ToLower().Contains("chauffeur") ? "WPS Driver" : details.employee_Name_English;
                                driverMobile = details.mobile;
                            }

                            items.cost_center = employeeDetails.cost_Center;
                            items.department = employeeDetails.department;
                            items.driver = driverName;
                            items.driver_mobile = driverMobile;
                            items.email = employeeDetails.e_Mail;
                            items.line_manager = employeeDetails.line_Manager_Name;
                            items.line_manager_email = employeeDetails.line_Manager_Email;
                            items.line_manager_id = employeeDetails.line_Manager_No;

                            items.complaint = fleetServeDetails.complaint;
                            items.end_km = fleetServeDetails.end_km;
                            items.note = fleetServeDetails.note;
                            items.plate = fleetServeDetails.plate;
                            items.sar = fleetServeDetails.sar;

                            listReturn.Add(items);

                            break;
                        }
                    }
                }

            }


            var request = new List<FleetRequestData>();
            request.Add(new FleetRequestData
            {
                data = listReturn
            });

            return request;
        }
          

        public static List<FleetRequest> PopulateFleetRequest(MySqlDataReader dr)
        {

            var listReturn = new List<FleetRequest>();

            while (dr.Read())
            {

                var fleetServeDetails = GetFleetServeDetails(dr["service_ref"].ToString());

                var fromDate = Convert.ToDateTime(dr["from_date"]);
                var nature = dr["nature"].ToString();
                if (fromDate.Date >= DateTime.Now.Date)
                {

                    listReturn.Add(new FleetRequest
                    {
                        code = dr["code"].ToString(),
                        contact = dr["contact"].ToString(),
                        
                        driver_id = dr["driver_id"].ToString(),
                        
                        drop = dr["drop"].ToString(),
                      
                        emp_id = dr["emp_id"].ToString(),
                        
                        from_date = Convert.ToDateTime(dr["from_date"].ToString()),
                        grade = int.Parse(dr["grade"].ToString()),
                        id = int.Parse(dr["id"].ToString()),
                        ip = dr["ip"].ToString(),
                        justification = dr["justification"].ToString(),
                        
                        name = dr["name"].ToString(),
                        nature = dr["nature"].ToString(),
                        
                        pax = dr["pax"].ToString(),
                        pick = dr["pick"].ToString(),
                       
                        random = dr["random"].ToString(),
                        randomize = dr["randomize"].ToString(),
                       
                        remarks = dr["remarks"].ToString(),
                       
                        service_ref = dr["service_ref"].ToString(),
                        session = dr["session"].ToString(),

                        specific_pick = dr["specific_pick"].ToString(),
                        
                        timestamp = dr["timestamp"].ToString(),
                        to_date = nature.ToLower().Contains("chauffeur") ? Convert.ToDateTime(dr["from_date"].ToString()) : Convert.ToDateTime(dr["to_date"].ToString()),
                        type = dr["type"].ToString(),

                        complaint = fleetServeDetails.complaint,
                        end_km = fleetServeDetails.end_km,
                        note = fleetServeDetails.note,
                        plate = fleetServeDetails.plate,
                        sar = fleetServeDetails.sar,

                    });
                }
            }

            return listReturn;
        }

        public static FleetRequest GetFleetServeDetails(string service_ref)
        {
            var objReturn = new FleetRequest();
            using (var con = new MySqlConnection(ConnectionHelper.MyDirectoryConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT * FROM fleet_serve " +
                                                "WHERE service_ref=@service_ref", con);
                cmd.Parameters.AddWithValue("@service_ref", service_ref);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    objReturn = new FleetRequest
                    {
                        plate = dr["plate"].ToString(),
                        start_km = int.Parse(dr["start_km"].ToString()),
                        end_km = int.Parse(dr["end_km"].ToString()),
                        sar = int.Parse(dr["sar"].ToString()),
                        complaint = dr["complaint"].ToString(),
                        note = dr["note"].ToString(),
                    };
                }
            }

            return objReturn;
        }

    }
}