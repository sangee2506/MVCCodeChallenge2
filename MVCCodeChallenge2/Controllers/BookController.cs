using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCodeChallenge2.Models;
using MVCCodeChallenge2.CustomExceptions;

namespace MVCCodeChallenge2.Controllers
{
    public class BookController : Controller
    {
        BookDataAccessLayer da = new BookDataAccessLayer();

        AuthorDataAccessLayer daAuthor = new AuthorDataAccessLayer();
        // GET: Book
        public ActionResult Index(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new AuthorNotFoundException("Author Not Found");
                }
                List<Book> booksList = da.GetAllBooks().ToList();
                List<Book> booksListOfSameID = new List<Book>();
               
                List<Author> authorList = daAuthor.GetAllAuthors().ToList();
                Author author = authorList.Find(x => x.IdNo == id);
                String authorName = author.Name;


                foreach (Book book in booksList)
                {
                    if (book.author.IdNo == id)
                    {
                       
                        booksListOfSameID.Add(book);
                    }
                }

                ViewBag.AuthorName = authorName;
                return View(booksListOfSameID);
            }
            catch(AuthorNotFoundException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }
           
        }

        

        // GET: Book/Create
        public ActionResult Create(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new AuthorNotFoundException("Author Not Found");
                }
                ViewBag.AuthorId = id;
                return View();
            }
            catch (AuthorNotFoundException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }

        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book,int id)
        {
            try
            {
                
                da.AddBookRecord(book,id);

                return RedirectToAction("Index",new { id = id });
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new BookNotFoundException("Book Not Found");
                }
                    List<Book> booksList = da.GetAllBooks().ToList();
                    Book book = booksList.Find(x => x.BookId == id);
                    ViewBag.AuthorId = book.author.IdNo;
                    return View(book);
                

            }
            catch (BookNotFoundException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }

        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,int authorId)
        {
            try
            {
                Book book = new Book();
                book.BookId = id;
                book.Title = collection["Title"];
                book.Genere = collection["Genere"];
                book.Price = Convert.ToDecimal(collection["Price"]);
               
                da.UpdateBookRecord(book);

                return RedirectToAction("Index", new { id = authorId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new BookNotFoundException("Book Not Found");
                }
                List<Book> booksList = da.GetAllBooks().ToList();
                Book book = booksList.Find(x => x.BookId == id);
                ViewBag.AuthorId = book.author.IdNo;
                return View(book);
            }
            catch (BookNotFoundException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }

        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int authorId)
        {
            try
            {

                da.DeleteBook(id);
                return RedirectToAction("Index", new { id = authorId });
            }
            catch
            {
                return View();
            }
        }

        //GET:Details
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new BookNotFoundException("Book Not Found");
                }
                List<Book> booksList = da.GetAllBooks().ToList();
                Book book = booksList.Find(x => x.BookId == id);
                ViewBag.AuthorId = book.author.IdNo;
                return View(book);
            }
            catch (BookNotFoundException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }

        }
    }
}
