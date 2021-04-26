using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCCodeChallenge2.Models
{
    public class BookDataAccessLayer
    {
        string connection = ConfigurationManager.ConnectionStrings["BookAuthorConnection"].ConnectionString;
        AuthorDataAccessLayer da = new AuthorDataAccessLayer();
        //TO get all Books
        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> BooksList = new List<Book>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("select * from Book", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Author> authorList = da.GetAllAuthors().ToList();
                while (sdr.Read())
                {
                    Book book = new Book();
                    book.BookId =(int) sdr["BookId"];
                    book.Title = (string)sdr["Title"];
                    book.Genere= (string)sdr["Genere"];
                    book.Price = Convert.ToDecimal(sdr["Price"]);
                    book.author= authorList.Find(x=>x.IdNo==(int)sdr["AuthorId"]);
                    BooksList.Add(book);
                }
                
            }
            return BooksList;

        }

        //To get Employee Data
      /*  public Book GetBookRecordById(int id)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("select * from tblBook where BookID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                Book Book = new Book();
                while (sdr.Read())
                {

                    Book.BookID = (int)sdr["BookId"];
                    Book.BookName = (string)sdr["Name"];
                    Book.Place = (string)sdr["Place"];

                }
                return Book;

            }
        }*/

        //To add new record into Book table
        public void AddBookRecord(Book Book,int? id)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "Insert into Book Values(@Title,@Genere,@Price,@AuthorId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", Book.Title);
                cmd.Parameters.AddWithValue("@Genere", Book.Genere);
                cmd.Parameters.AddWithValue("@Price", Book.Price);
                cmd.Parameters.AddWithValue("@AuthorId", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //To Update Book Record
        public void UpdateBookRecord(Book book)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query= "Update Book set Title = @Title, Genere = @Genere,Price=@Price  where BookId = @BookId";
                SqlCommand cmd = new SqlCommand(query, con);
                

                cmd.Parameters.AddWithValue("@BookId ",book.BookId);
                cmd.Parameters.AddWithValue("@Title",book.Title);
                cmd.Parameters.AddWithValue("@Genere",book.Genere);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //To Delete Book Record
        public void DeleteBook(int id)
        {

            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "Delete from Book Where BookID=@BookId";
                SqlCommand cmd = new SqlCommand(query, con);
                

                cmd.Parameters.AddWithValue("@BookId", id);

                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}