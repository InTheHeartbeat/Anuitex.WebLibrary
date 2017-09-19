using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.Models.IO.Export;
using Anuitex.WebLibrary.Models.IO.Export.Books;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class ExportController : BaseController
    {
        public ActionResult Index(string productType)
        {
            if (productType == typeof(Book).Name)
            {
                return RedirectToAction("Books");
            }
            if (productType == typeof(Journal).Name)
            {
                return RedirectToAction("Journals");
            }
            if (productType == typeof(Newspaper).Name)
            {
                return RedirectToAction("Newspapers");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Books()
        {
            return View("ExportBooks", new ExportBooksModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Books", "Export", null, Request.Url.Scheme)),
                CurrentNavSection = NavSection.Export,
                CurrentUser = CurrentUser,
                Books = DataContext.Books.Select(book => new ExportableBookModel(book)).ToList()
            });
        }

        public ActionResult Journals()
        {
            return View("ExportJournals", new ExportJournalsModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Journals", "Export", null, Request.Url.Scheme)),
                CurrentNavSection = NavSection.Export,
                CurrentUser = CurrentUser,
                Journals = DataContext.Journals.Select(journal => new JournalModel(journal)).ToList()
            });
        }

        public ActionResult Newspapers()
        {
            return View("ExportNewspapers", new ExportNewspapersModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Newspapers", "Export", null, Request.Url.Scheme)),
                CurrentNavSection = NavSection.Export,
                CurrentUser = CurrentUser,
                Newspapers = DataContext.Newspapers.Select(newspaper => new NewspaperModel(newspaper)).ToList()
            });
        }

        [HttpPost]
        public FileResult TryExportBooks(ExportBooksModel model)
        {
            if (model.IsXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BookModel>));
                MemoryStream stream = new MemoryStream();
                serializer.Serialize(stream, model.Books.Where(bk => bk.Selected)
                    .Select(bk => new BookModel(DataContext.Books.First(book=>book.Id == bk.Id)))
                    .ToList());
                return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now.ToShortDateString() + "-books.xml");
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.Default);

                writer.WriteLine("Books");

                foreach (BookModel book in model.Books.Where(bk=>bk.Selected).Select(sbk=> new BookModel(DataContext.Books.First(book => book.Id == sbk.Id)))
                    .ToList())
                {
                    writer.WriteLine(book.Id);
                    writer.WriteLine(book.Title);
                    writer.WriteLine(book.Year);
                    writer.WriteLine(book.Pages);
                    writer.WriteLine(book.Author);
                    writer.WriteLine(book.Genre);
                    writer.WriteLine(book.Amount);
                    writer.WriteLine(book.Price);
                    writer.WriteLine(book.PhotoId);
                    writer.WriteLine(book.PhotoPath);
                }
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now.ToShortDateString() + "-books.txt");
            }
        }
    }
}