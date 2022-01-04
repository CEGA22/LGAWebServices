using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class SectionsHandled
    {
        public int ID { get; set; }

        public int TeacherID { get; set; }

        public string GradeLevel { get; set; }

        public int GradeLevelID { get; set; }

        public string SectionName { get; set; }

        public List<SectionsHandled> GetSectionsHandled(int ID)

        {
            var listReturn = new List<SectionsHandled>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT SectionsHandled.ID, SectionsHandled.Teacher, YearLevel.Grade_Level, SectionsHandled.Gradelevel, Section.SectionName FROM SectionsHandled JOIN Section ON SectionsHandled.Gradelevel = Section.ID JOIN YearLevel ON Section.Grade_Level = YearLevel.ID WHERE SectionsHandled.Teacher = @TeacherID";                
                cmd.Parameters.AddWithValue("@TeacherID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }         
            return listReturn;
        }

        public List<SectionsHandled> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<SectionsHandled>();

                while (dr.Read())
                {

                    listReturn.Add(new SectionsHandled
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        TeacherID = int.Parse(dr["Teacher"].ToString()),
                        GradeLevel = dr["Grade_Level"].ToString(),
                        GradeLevelID = int.Parse(dr["Gradelevel"].ToString()),
                        SectionName = dr["SectionName"].ToString()
                    });
                }

                return listReturn;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}