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
    public class EECEmployee
    {
        public string employee_Name_English { get; set; }
        public string employee_Name_Arabic { get; set; }
        public string emp_ID { get; set; }
        
        public string position { get; set; }
        public string positionArabic { get; set; }
        public string cost_Center { get; set; }
        public string department { get; set; }
        public string division { get; set; }
        public string site_English { get; set; }
        public string site_Arabic { get; set; }
        public string site_Abbreviation { get; set; }
        public string line_Manager_Name { get; set; }


        public string e_Mail { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string desk { get; set; }


        public string birth_Date { get; set; }
        public string cost_Center_HoD { get; set; }
        public string dept_Head_Email { get; set; }
        public string dept_Head_Name { get; set; }
        public string dept_Head_Name_AP { get; set; }
        public string dept_Head_No { get; set; }
        public string employee_Spouse { get; set; }
        public string function_Head { get; set; }
        public string gender { get; set; }
        public string grade { get; set; }
        public string hiring_Date { get; set; }
        public string id { get; set; }
        public string iD_Iqama { get; set; }
        public byte[] imageByte { get; set; }
        public string imageLocation { get; set; }
        public string line_Manager_Email { get; set; }
        public string line_Manager_Name_AP { get; set; }
        public string line_Manager_No { get; set; }
        public string nationality { get; set; }
        public string oracle_Org_Name { get; set; }
        public string section { get; set; }
        public string sponsor { get; set; }
        public string termination_Date { get; set; }


        public List<EECEmployee> SearchOnDirectory(string keyWord)
        {
            var lisReturn = new List<EECEmployee>();
            var strWhere = string.Empty;

            try
            {
                int.Parse(keyWord);
                strWhere = @" Emp_ID='" + keyWord + "' AND Termination_Date IS NULL AND Emp_ID NOT LIKE '%A%' AND Emp_ID NOT LIKE '%E%' AND Emp_ID NOT LIKE '%R%' AND Emp_ID NOT LIKE '%T%' AND Emp_ID != '5050' AND Emp_ID != '2014'";
            }

            catch
            {
                strWhere = @" (Employee_Name_English LIKE '%" + keyWord + "%' OR E_Mail LIKE '%" + keyWord + "%' OR Position LIKE '%" + keyWord + "%' OR Department LIKE '%" + keyWord + "%' OR Division LIKE '%" + keyWord + "%' OR Emp_ID LIKE '%" + keyWord + "%') AND Termination_Date IS NULL AND Emp_ID NOT LIKE '%A%' AND Emp_ID NOT LIKE '%E%' AND Emp_ID NOT LIKE '%R%' AND Emp_ID NOT LIKE '%T%' AND Emp_ID != '5050' AND Emp_ID != '2014'";
            }

            using (var cn = new SqlConnection(ConnectionHelper.HRDBConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.Employee_Info_Extended WHERE" + strWhere;
                var dr = cmd.ExecuteReader();
                lisReturn = PopulateReturnList(dr);
            }

            return lisReturn;
        }

        public List<EECEmployee> GetEmployeeDetails(string employeeMail)
        {
            var listReturn = new List<EECEmployee>();

            using (var cn = new SqlConnection(ConnectionHelper.HRDBConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.Employee_Info_Extended WHERE e_Mail=@e_Mail OR emp_ID=@e_Mail";
                cmd.Parameters.AddWithValue("@e_Mail", employeeMail);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<EECEmployee> GetDesk(string empId)
        {
            var listReturn = new List<EECEmployee>();
            using (var con = new MySqlConnection(ConnectionHelper.MyDirectoryConnection()))
            {
                con.Open();

                using (var cmd = new MySqlCommand("SELECT desk, phone FROM data WHERE emp_id=@emp_id", con))
                {
                    cmd.Parameters.AddWithValue("@emp_id", empId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listReturn.Add(new EECEmployee
                            {
                                desk = dr["desk"].ToString(),
                                phone = dr["phone"].ToString(),
                            });
                        }

                        dr.Close();
                    }
                }
                con.Close();
            }

            return listReturn;
        }

        public EECEmployee GetMobile(string empId)
        {
            var objReturn = new EECEmployee();
            using (var cn = new SqlConnection(ConnectionHelper.HRDBConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Mobile FROM dbo.Employee_Info_Extended WHERE Emp_ID=@emp_id";
                cmd.Parameters.AddWithValue("@emp_id", empId);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objReturn = new EECEmployee
                    {
                        mobile = dr["Mobile"].ToString()
                    };
                }
            }
            return objReturn;
        }

        public List<EECEmployee> GetSharesLineManager(string lineManagerNo)
        {
            var lisReturn = new List<EECEmployee>();
            using (var cn = new SqlConnection(ConnectionHelper.HRDBConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("@Line_Manager_No", lineManagerNo);
                cmd.CommandText = "SELECT * FROM dbo.Employee_Info_Extended WHERE Line_Manager_No=@Line_Manager_No AND Termination_Date IS NULL AND Emp_ID NOT LIKE '%A%' AND Emp_ID NOT LIKE '%E%' AND Emp_ID NOT LIKE '%R%' AND Emp_ID NOT LIKE '%T%' AND Emp_ID != '5050' AND Emp_ID != '2014'";
                var dr = cmd.ExecuteReader();
                lisReturn = PopulateReturnList(dr);
            }

            return lisReturn;
        }

        public List<EECEmployee> GetDirectReports(string empId)
        {
            var lisReturn = new List<EECEmployee>();
            using (var cn = new SqlConnection(ConnectionHelper.HRDBConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("@Emp_ID", empId);
                cmd.CommandText = "SELECT * FROM dbo.Employee_Info_Extended WHERE Line_Manager_No=@Emp_ID AND Termination_Date IS NULL AND Emp_ID NOT LIKE '%A%' AND Emp_ID NOT LIKE '%E%' AND Emp_ID NOT LIKE '%R%' AND Emp_ID NOT LIKE '%T%' AND Emp_ID != '5050' AND Emp_ID != '2014'";
                var dr = cmd.ExecuteReader();
                lisReturn = PopulateReturnList(dr);
            }

            return lisReturn;
        }

        public List<EECEmployee> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<EECEmployee>();

            while (dr.Read())
            {

                var deskInformation = GetDesk(dr["Emp_ID"].ToString());
                var desk = deskInformation.Count == 0 ? "" : deskInformation[0].desk;
                var phone = deskInformation.Count == 0 ? "" : deskInformation[0].phone;

                string imgUri = Constants.employeeImageUri + dr["Emp_ID"].ToString() + ".jpg";
                byte[] imageBytes = null;

                using (var webClient = new WebClient())
                {
                    try
                    {
                        imageBytes = webClient.DownloadData(imgUri);
                    }

                    catch { }
                }

                listReturn.Add(new EECEmployee
                {
                    birth_Date = string.IsNullOrWhiteSpace(dr["Birth_Date"].ToString()) ? "" : Convert.ToDateTime(dr["Birth_Date"].ToString()).Date.ToString("yyyy-MM-dd"),
                    cost_Center = dr["Cost_Center"].ToString(),
                    cost_Center_HoD = dr["Cost_Center_HoD"].ToString(),
                    department = dr["Department"].ToString(),
                    dept_Head_Email = dr["Dept_Head_Email"].ToString(),
                    dept_Head_Name = dr["Dept_Head_Name"].ToString(),
                    dept_Head_Name_AP = dr["Dept_Head_Name_AP"].ToString(),
                    dept_Head_No = dr["Dept_Head_No"].ToString(),
                    desk = desk,
                    division = dr["Division"].ToString(),
                    employee_Name_Arabic = dr["Employee_Name_Arabic"].ToString(),
                    employee_Name_English = dr["Employee_Name_English"].ToString(),
                    employee_Spouse = dr["Employee_Spouse"].ToString(),
                    emp_ID = dr["Emp_ID"].ToString(),
                    e_Mail = dr["E_Mail"].ToString(),
                    function_Head = dr["Function_Head"].ToString(),
                    gender = dr["Gender"].ToString(),
                    grade = dr["Grade "].ToString(),
                    hiring_Date = string.IsNullOrWhiteSpace(dr["Hiring_Date"].ToString()) ? "" : Convert.ToDateTime(dr["Hiring_Date"].ToString()).Date.ToString("yyyy-MM-dd"),
                    iD_Iqama = dr["ID_Iqama"].ToString(),
                    imageByte = imageBytes,
                    imageLocation = Constants.employeeImageUri + dr["Emp_ID"].ToString() + ".jpg",
                    line_Manager_Email = dr["Line_Manager_Email"].ToString(),
                    line_Manager_Name = dr["Line_Manager_Name "].ToString(),
                    line_Manager_Name_AP = dr["Line_Manager_Name_AP"].ToString(),
                    line_Manager_No = dr["Line_Manager_No"].ToString(),
                    mobile = dr["Mobile"].ToString(),
                    nationality = dr["Nationality"].ToString(),
                    oracle_Org_Name = dr["Oracle_Org_Name"].ToString(),
                    phone = phone,
                    position = dr["Position"].ToString(),
                    positionArabic = dr["PositionArabic"].ToString(),
                    section = dr["Section"].ToString(),
                    site_Abbreviation = dr["Site_Abbreviation"].ToString(),
                    site_Arabic = dr["Site_Arabic"].ToString(),
                    site_English = dr["Site_English"].ToString(),
                    sponsor = dr["Sponsor"].ToString(),
                    termination_Date = string.IsNullOrWhiteSpace(dr["Termination_Date"].ToString()) ? "" : Convert.ToDateTime(dr["Termination_Date"].ToString()).Date.ToString("yyyy-MM-dd"),
                });
            }

            return listReturn;
        }

      

    }
}