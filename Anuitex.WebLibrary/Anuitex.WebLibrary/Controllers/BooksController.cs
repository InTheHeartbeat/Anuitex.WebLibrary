using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.ViewHelpers;
using Microsoft.Ajax.Utilities;

namespace Anuitex.WebLibrary.Controllers
{
    public class BooksController : BaseController
    {
        // GET: Books
        public ActionResult Index()
        {            
            return View(new BooksModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Index", "Books", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                Books = DataContext.Books.Select(book => new BookModel(book)).ToList(),
                CurrentNavSection = NavSection.Books
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
                CurrentUser = CurrentUser,
                IsEdit = false,
                CurrentNavSection = NavSection.Books
            });
        }

        [HttpPost]
        public ActionResult TryAddBook(AddBookModel model)
        {
            if (model == null) { return RedirectToAction("AddBook", "Books");}
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToAction("Index", "Books"); }

            DataContext.Books.InsertOnSubmit(new Book()
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
            DataContext.SubmitChanges();

            return RedirectToAction("Index", "Books");
        }
        
        [Route("Books/RemoveBook/{id:int}")]
        public ActionResult RemoveBook(int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {return RedirectToActionPermanent("Index");}

            Book book = DataContext.Books.FirstOrDefault(bk => bk.Id == id);

            if (book == null)
            {return RedirectToActionPermanent("Index");}

            DataContext.Books.DeleteOnSubmit(book);
            if(book.Image != null && book.Image.Id > 0)
            { DataContext.Images.DeleteOnSubmit(book.Image);}
            DataContext.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditBook(int? id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToActionPermanent("Index"); }

            Book book = DataContext.Books.FirstOrDefault(bk => bk.Id == id);

            if (book == null)
            { return RedirectToActionPermanent("Index"); }

            return View("AddBook", new AddBookModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("EditBook", "Books", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                IsEdit = true,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Pages = book.Pages,
                Year = book.Year,
                Amount = book.Amount,
                Price = book.Price,
                PhotoId = book.PhotoId,
                Id = book.Id,
                PhotoPath = book.Image.Path
            });
        }

        [HttpPost]
        public ActionResult TryEditBook(AddBookModel model)
        {
            if (model != null)
            {
                Book book = DataContext.Books.FirstOrDefault(bk => bk.Id == model.Id);
                book.Title = model.Title;
                book.Author = model.Author;
                book.Genre = model.Genre;
                book.Pages = model.Pages;
                book.Year = model.Year;
                book.Price = model.Price;
                book.Amount = model.Amount;
                book.PhotoId = model.PhotoId;
                DataContext.SubmitChanges();

                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("EditBook", "Books");
        }
    }
}