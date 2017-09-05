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
                CurrentUser = CurrentUser,
                IsEdit = false
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


        
        [Route("Books/RemoveBook/{id:int}")]
        public ActionResult RemoveBook(int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {return RedirectToActionPermanent("Index");}

            Book book = DataContext.Context.LibraryDataContext.Books.FirstOrDefault(bk => bk.Id == id);

            if (book == null)
            {return RedirectToActionPermanent("Index");}

            DataContext.Context.LibraryDataContext.Books.DeleteOnSubmit(book);
            DataContext.Context.LibraryDataContext.Images.DeleteOnSubmit(
                DataContext.Context.LibraryDataContext.Images.FirstOrDefault(img => img.Id == book.PhotoId));
            DataContext.Context.LibraryDataContext.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditBook(int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToActionPermanent("Index"); }

            Book book = DataContext.Context.LibraryDataContext.Books.FirstOrDefault(bk => bk.Id == id);

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
                PhotoId = book.PhotoId.Value,
                Id = book.Id
            });
        }

        [HttpPost]
        public ActionResult TryEditBook(AddBookModel model)
        {
            if (model != null)
            {
                Book book = DataContext.Context.LibraryDataContext.Books.FirstOrDefault(bk => bk.Id == model.Id);
                book.Title = model.Title;
                book.Author = model.Author;
                book.Genre = model.Genre;
                book.Pages = model.Pages;
                book.Year = model.Year;
                book.Price = model.Price;
                book.Amount = model.Amount;
                book.PhotoId = model.PhotoId;
                DataContext.Context.LibraryDataContext.SubmitChanges();
                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("EditBook", "Books");
        }
    }
}