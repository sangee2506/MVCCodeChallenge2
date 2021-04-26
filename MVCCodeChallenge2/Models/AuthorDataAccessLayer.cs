using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCCodeChallenge2.Models
{
    public class AuthorDataAccessLayer
    {
        string connection = ConfigurationManager.ConnectionStrings["BookAuthorConnection"].ConnectionString;
        //To get Authors List
        public IEnumerable<Author> GetAllAuthors()
        {
            List<Author> AuthorsList = new List<Author>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("select * from Author", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Author author = new Author();
                    author.IdNo = (int)sdr["Idno"];
                    author.Name = (string)sdr["Name"];
                    author.Address = (string)sdr["Address"];
                    AuthorsList.Add(author);

                }

            }
            return AuthorsList;

        }

        //To add new record into Author table
        public void AddAuthorRecord(Author author)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("spAddAuthor", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Name", author.Name);
                cmd.Parameters.AddWithValue("@Address", author.Address);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //To Update Author Record
        public void UpdateAuthorRecord(Author author)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "Update Author set Name = @Name,Address=@Address where IdNo=@AuthorId";
                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@AuthorId ",author.IdNo);
                cmd.Parameters.AddWithValue("@Name", author.Name);
                cmd.Parameters.AddWithValue("@Address", author.Address);
               


                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}