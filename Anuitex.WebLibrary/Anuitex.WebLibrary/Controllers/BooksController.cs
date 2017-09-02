using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Models;

namespace Anuitex.WebLibrary.Controllers
{
    public class BooksController : BaseController
    {
        // GET: Books
        public ActionResult Index()
        {
            return View(new BooksModel()
            {
                Books = DataContext.Context.LibraryDataContext.Books.ToList(),
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Index", "Books", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser
            });
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToAction("Index", "Books");}
                
            return View(new AddBookModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("AddBook", "Books", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser
            });
        }

        [HttpPost]
        public ActionResult TryAddBook(AddBookModel model)
        {
            if (model != null)
            {
                DataContext.Context.LibraryDataContext.Books.InsertOnSubmit(new Book()
                {
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    Amount = model.Amount,
                    Pages = model.Pages,
                    Price = model.Price,
                    Year = model.Year,
                    PhotoId = model.PhotoId
                });
                DataContext.Context.LibraryDataContext.SubmitChanges();
                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("AddBook", "Books");
        }
    }
}