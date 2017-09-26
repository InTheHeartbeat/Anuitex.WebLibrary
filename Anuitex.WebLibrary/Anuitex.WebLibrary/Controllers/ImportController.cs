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
using Anuitex.WebLibrary.Models.IO.Export.Journals;
using Anuitex.WebLibrary.Models.IO.Export.Newspapers;
using Anuitex.WebLibrary.Models.IO.Import.Books;
using Anuitex.WebLibrary.Models.IO.Import.Journals;
using Anuitex.WebLibrary.Models.IO.Import.Newspapers;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class ImportController : BaseController
    {
        [HttpPost]
        public ActionResult Books(HttpPostedFileBase upload)
        {
            if (upload == null) {return RedirectToAction("Index","Books");}

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
            if (model != null)
            {
                DataContext.Books.InsertAllOnSubmit(model.Books.Where(bs => bs.Selected)
                    .Select(book => new Book()
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
            }
            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public ActionResult Journals(HttpPostedFileBase upload)
        {
            if (upload == null) { return RedirectToAction("Index", "Journals"); }

            try
            {
                FileInfo info = new FileInfo(upload.FileName);
                List<JournalModel> imported = ImportHelper.ImportJournals(upload.InputStream, info.Extension == ".xml");
                if (imported != null)
                {
                    return View("ImportJournalsResult", new ImportJournalsResultModel()
                    {
                        BreadcrumbModel = new BreadcrumbModel(Url.Action("Journals", "Import", null, Request.Url.Scheme)),
                        CurrentNavSection = NavSection.Import,
                        CurrentUser = CurrentUser,
                        Journals = imported.Select(b => new ExportableJournalModel()
                            {
                                Id = b.Id,
                                Amount = b.Amount,
                                PhotoId = b.PhotoId,
                                Subjects = b.Subjects,
                                Periodicity = b.Periodicity,                                
                                Title = b.Title,
                                Date = b.Date,
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
        public ActionResult TryImportJournals(ImportJournalsResultModel model)
        {
            if (model != null)
            {
                DataContext.Journals.InsertAllOnSubmit(model.Journals.Where(jour => jour.Selected)
                    .Select(journal => new Journal()
                    {
                        Amount = journal.Amount,
                        Periodicity = journal.Periodicity,
                        Subjects = journal.Subjects,
                        Date = journal.Date,
                        PhotoId = journal.PhotoId,
                        Price = journal.Price,
                        Title = journal.Title
                    }));
                DataContext.SubmitChanges();
            }
            return RedirectToAction("Index", "Journals");
        }

        [HttpPost]
        public ActionResult Newspapers(HttpPostedFileBase upload)
        {
            if (upload == null) { return RedirectToAction("Index", "Newspapers"); }

            try
            {
                FileInfo info = new FileInfo(upload.FileName);
                List<NewspaperModel> imported = ImportHelper.ImportNewspapers(upload.InputStream, info.Extension == ".xml");
                if (imported != null)
                {
                    return View("ImportNewspapersResult", new ImportNewspapersResultModel()
                    {
                        BreadcrumbModel = new BreadcrumbModel(Url.Action("Newspapers", "Import", null, Request.Url.Scheme)),
                        CurrentNavSection = NavSection.Import,
                        CurrentUser = CurrentUser,
                        Newspapers = imported.Select(b => new ExportableNewspaperModel()
                            {
                                Id = b.Id,
                                Amount = b.Amount,
                                PhotoId = b.PhotoId,                                
                                Periodicity = b.Periodicity,
                                Title = b.Title,
                                Date = b.Date,
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
        public ActionResult TryImportNewspapers(ImportNewspapersResultModel model)
        {
            if (model != null)
            {
                DataContext.Newspapers.InsertAllOnSubmit(model.Newspapers.Where(np => np.Selected)
                    .Select(newspaper => new Newspaper()
                    {
                        Amount = newspaper.Amount,
                        Periodicity = newspaper.Periodicity,
                        Date = newspaper.Date,
                        PhotoId = newspaper.PhotoId,
                        Price = newspaper.Price,
                        Title = newspaper.Title
                    }));
                DataContext.SubmitChanges();
            }
            return RedirectToAction("Index", "Newspapers");
        }
    }
}