using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace EZWebServices.Models.Employees
{
    public class ManagerCentre
    {

        public string EmployeeeID { get; set; }
        public byte[] UserPhoto { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string  Position { get; set; }
        public string PositionArabic { get; set; }
        public string Grade { get; set; }
        public string HireDate { get; set; }
        public string Department { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string ManagerID { get; set; }
        public string ManagerPosition { get; set; }
        public byte[] ManagerPhoto { get; set; }
        public string ManagerName { get; set; }
        public List<DirectReports> DirectReports { get; set; }
        public int NoOfDirectorReports { get; set; }
        public List<SharesLineManager> SharesLineManager { get; set; }
       

        public ManagerCentre GetManagerCentreDetails(string id)
        {
            var objReturn = new ManagerCentre();
            var listDirectReports = new List<DirectReports>();
            var listSharesLineManager = new List<SharesLineManager>();

            try
            {

                int.Parse(id);

                var eecEmployee = new EECEmployee();
                var employeeDetails = eecEmployee.GetEmployeeDetails(id).FirstOrDefault();

                var managerDetails = eecEmployee.GetEmployeeDetails(employeeDetails.line_Manager_No).FirstOrDefault();

                var directReports = eecEmployee.GetDirectReports(id);
                var sharesLineManager = eecEmployee.GetSharesLineManager(employeeDetails.line_Manager_No);

                foreach (var items in directReports)
                {
                    listDirectReports.Add(new DirectReports
                    {
                        CostCenter = items.cost_Center,
                        Department = items.department,
                        EmployeeeID = items.emp_ID,
                        Grade = items.grade,
                        HireDate = items.hiring_Date,
                        Name = items.employee_Name_English,
                        NameArabic = items.employee_Name_Arabic,
                        Position = items.position,
                        PositionArabic = items.positionArabic,
                        UserPhoto = items.imageByte
                    });
                }

                foreach (var items in sharesLineManager)
                {
                    listSharesLineManager.Add(new SharesLineManager
                    {
                        CostCenter = items.cost_Center,
                        Department = items.department,
                        EmployeeeID = items.emp_ID,
                        Grade = items.grade,
                        HireDate = items.hiring_Date,
                        Name = items.employee_Name_English,
                        NameArabic = items.employee_Name_Arabic,
                        Position = items.position,
                        PositionArabic = items.positionArabic,
                        UserPhoto = items.imageByte
                    });
                }


                objReturn = new ManagerCentre
                {
                    Birthday = employeeDetails.birth_Date,
                    CostCenter = employeeDetails.cost_Center,
                    Department = employeeDetails.department,
                    Email = employeeDetails.e_Mail,
                    EmployeeeID = employeeDetails.emp_ID,
                    Gender = employeeDetails.gender,
                    Grade = employeeDetails.grade,
                    HireDate = employeeDetails.hiring_Date,
                    ManagerID = employeeDetails.line_Manager_No,
                    ManagerName = employeeDetails.line_Manager_Name,
                    ManagerPhoto = managerDetails.imageByte,
                    ManagerPosition = managerDetails.position,
                    Mobile = employeeDetails.mobile,
                    Name = employeeDetails.employee_Name_English,
                    NameArabic = employeeDetails.employee_Name_Arabic,
                    Nationality = employeeDetails.nationality,
                    Position = employeeDetails.position,
                    PositionArabic = employeeDetails.positionArabic,
                    UserPhoto = employeeDetails.imageByte,

                    DirectReports = listDirectReports,
                    NoOfDirectorReports = listDirectReports.Count,
                    SharesLineManager = listSharesLineManager,
                };

            }

            catch
            {
                objReturn = null;
            }

            return objReturn;
        }
    }
}