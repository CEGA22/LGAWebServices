using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace EZWebServices.Models.Employees
{
    public class NonEECEmployee
    {
        public int id { get; set; }
        public string status { get; set; }
        public string network_id { get; set; }
        public string email { get; set; }
        public string emp_id { get; set; }
        public int nedap { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string cost_center { get; set; }
        public string department { get; set; }
        public string desk { get; set; }
        public string phone { get; set; }
        public int mobile { get; set; }
        public string manager { get; set; }
        public string manager_id { get; set; }
        public DateTime hire_date { get; set; }
        public string position { get; set; }
        public string grade { get; set; }
        public string name_ara { get; set; }
        public string position_arabic { get; set; }
        public string type { get; set; }
        public string sponsor { get; set; }
        public string nationality { get; set; }
        public string gender { get; set; }
        public DateTime birthdate { get; set; }
        public string pin { get; set; }
        public string section { get; set; }
        public string division { get; set; }
        public DateTime end_date { get; set; }


        public NonEECEmployee GetEmployeeDetails(string employeeId)
        {
            var objReturn = new NonEECEmployee();

            using (var cn = new MySqlConnection(ConnectionHelper.MyDirectoryConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM data WHERE emp_id=@emp_id";
                cmd.Parameters.AddWithValue("@emp_id", employeeId);
                var dr = cmd.ExecuteReader();
                try
                {
                    objReturn = PopulateReturnList(dr).FirstOrDefault();
                }

                catch(Exception e)
                {
                    return objReturn;
                }
            }

            return objReturn;
        }

        public List<NonEECEmployee> PopulateReturnList(MySqlDataReader dr)
        {

            var listReturn = new List<NonEECEmployee>();

            while (dr.Read())
            {

                string imgUri = Constants.employeeImageUri + dr["Emp_ID"].ToString() + ".jpg";
                byte[] imageBytes;

                using (var webClient = new WebClient())
                {
                    try
                    {
                        imageBytes = webClient.DownloadData(imgUri);
                    }

                    catch { }
                }

                listReturn.Add(new NonEECEmployee
                {
                    birthdate = Convert.ToDateTime(dr["birth_date"].ToString()),
                    code = dr["code"].ToString(),
                    cost_center = dr["cost_center"].ToString(),
                    department = dr["department"].ToString(),
                    desk = dr["desk"].ToString(),
                    division = dr["division"].ToString(),
                    email = dr["email"].ToString(),
                    emp_id = dr["emp_id"].ToString(),
                    end_date= Convert.ToDateTime(dr["end_date"].ToString()),
                    gender = dr["gender"].ToString(),
                    grade = dr["grade"].ToString(),
                    hire_date = Convert.ToDateTime(dr["hire_date"].ToString()),
                    id= int.Parse(dr["id"].ToString()),
                    manager = dr["manager"].ToString(),
                    manager_id = dr["manager_id"].ToString(),
                    mobile = !string.IsNullOrEmpty(dr["mobile"].ToString()) ? int.Parse(dr["mobile"].ToString()) : 0,
                    name = dr["name"].ToString(),
                    name_ara = dr["name_ara"].ToString(),
                    nationality = dr["nationality"].ToString(),
                    nedap = int.Parse(dr["nedap"].ToString()),
                    network_id = dr["network_id"].ToString(),
                    phone = dr["phone"].ToString(),
                    pin = dr["pin"].ToString(),
                    position = dr["position"].ToString(),
                    position_arabic = dr["position_arabic"].ToString(),
                    section = dr["section"].ToString(),
                    sponsor = dr["sponsor"].ToString(),
                    status = dr["status"].ToString(),
                    type = dr["type"].ToString(),
                });
            }

            return listReturn;
        }
    }
}