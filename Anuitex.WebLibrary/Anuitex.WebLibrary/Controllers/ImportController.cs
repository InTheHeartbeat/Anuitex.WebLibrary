using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;
using Anuitex.WebLibrary.Helpers;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.Models.IO.Export.Books;
using Anuitex.WebLibrary.Models.IO.Import.Books;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class ImportController : BaseController
    {
        [HttpPost]
        public ActionResult Books(HttpPostedFileBase upload)
        {
            try
            {
                FileInfo info = new FileInfo(upload.FileName);
                List<BookModel> imported = ImportHelper.ImportBooks(upload.InputStream, info.Extension == ".xml");
                if (imported != null)
                {
                    return View("ImportBooksResult", new ImportBooksResultModel()
                    {
                        BreadcrumbModel = new BreadcrumbModel(Url.Action("Books", "Import", null, Request.Url.Scheme)),
                        CurrentNavSection = NavSection.Import,
                        CurrentUser = CurrentUser,
                        Books = imported.Select(b => new ExportableBookModel()
                            {
                                Id = b.Id,
                                Amount = b.Amount,
                                PhotoId = b.PhotoId,
                                Pages = b.Pages,
                                Genre = b.Genre,
                                Author = b.Author,
                                Title = b.Title,
                                Year = b.Year,
                                Price = b.Price,
                                Selected = true,
                                PhotoPath = b.PhotoPath
                            })
                            .ToList()
                    });
                }
            }
            catch (Exception e)
            {
                return View("Error", new BaseModel()
                {
                    CurrentUser = CurrentUser
                });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult TryImportBooks(ImportBooksResultModel model)
        {
            DataContext.Books.InsertAllOnSubmit(model.Books.Where(bs=>bs.Selected).Select(book=>new Book()
            {
                Amount = book.Amount,
                Author = book.Author,
                Genre = book.Genre,
                Pages = book.Pages,
                PhotoId = book.PhotoId,
                Price = book.Price,
                Title = book.Title,
                Year = book.Year
            }));
            DataContext.SubmitChanges();
            return RedirectToAction("Index", "Books");
        }
    }
}