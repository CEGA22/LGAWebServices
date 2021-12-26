using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class NewsAndAnnouncements
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public byte[] ContentPhoto { get; set; }

        public string AuthorsName { get; set; }

        public DateTime DateCreated { get; set; }

        public List<NewsAndAnnouncements> GetNewsAndAnnouncements()
        {
            var listReturn = new List<NewsAndAnnouncements>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM NewsAndAnnouncements";               
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<NewsAndAnnouncements> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<NewsAndAnnouncements>();

            while (dr.Read())
            {

                listReturn.Add(new NewsAndAnnouncements
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    Title = dr["Title"].ToString(),
                    Content = dr["Content"].ToString(),
                    ContentPhoto = (byte[])dr["ContentPhoto"],
                    AuthorsName = dr["AuthorsName"].ToString(),
                    DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()),                    
                });
            }
            return listReturn;
        }

        public bool UpdateNewsAndAnnouncement(NewsAndAnnouncements request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("UPDATE NewsAndAnnouncements SET Title = @Title, Content = @Content, ContentPhoto = @ContentPhoto, AuthorsName = @AuthorsName, DateCreated = @DateCreated WHERE ID = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", request.ID);
                    cmd.Parameters.AddWithValue("@Title", request.Title);
                    cmd.Parameters.AddWithValue("@Content", request.Content);
                    cmd.Parameters.AddWithValue("@ContentPhoto", request.ContentPhoto);
                    cmd.Parameters.AddWithValue("@AuthorsName", request.AuthorsName);
                    cmd.Parameters.AddWithValue("@DateCreated", request.DateCreated);                   
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //studentId = (decimal)cmd.ExecuteScalar();
                    con.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}