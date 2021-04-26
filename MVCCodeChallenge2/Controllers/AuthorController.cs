using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCodeChallenge2.CustomExceptions;
using MVCCodeChallenge2.Models;

namespace MVCCodeChallenge2.Controllers
{
    public class AuthorController : Controller
    {
        AuthorDataAccessLayer da = new AuthorDataAccessLayer();
        // GET: AuthorsList
        public ActionResult Index()
        {
            List<Author> authorList = da.GetAllAuthors().ToList();
            return View(authorList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]//POST:Add new Author
        public ActionResult Create(Author author)
        {
            try
            {
                da.AddAuthorRecord(author);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new AuthorNotFoundException("Author Not Found");
                }
                List<Author> authorsList = da.GetAllAuthors().ToList();
                Author author = authorsList.Find(x => x.IdNo == id);
               // ViewBag.AuthorId = book.author.IdNo;
                return View(author);


            }
            catch (AuthorNotFoundException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }

        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Author author = new Author();
                author.IdNo = id;
                author.Name = collection["Name"];
                author.Address = collection["Address"];
               

                da.UpdateAuthorRecord(author);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}