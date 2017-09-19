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
using Anuitex.WebLibrary.Helpers;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.Models.IO.Export;
using Anuitex.WebLibrary.Models.IO.Export.Books;
using Anuitex.WebLibrary.Models.IO.Export.Journals;
using Anuitex.WebLibrary.Models.IO.Export.Newspapers;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class ExportController : BaseController
    {        
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
                Journals = DataContext.Journals.Select(journal => new ExportableJournalModel(journal)).ToList()
            });
        }

        public ActionResult Newspapers()
        {
            return View("ExportNewspapers", new ExportNewspapersModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Newspapers", "Export", null, Request.Url.Scheme)),
                CurrentNavSection = NavSection.Export,
                CurrentUser = CurrentUser,
                Newspapers = DataContext.Newspapers.Select(newspaper => new ExportableNewspaperModel(newspaper)).ToList()
            });
        }

        [HttpPost]
        public FileResult TryExportBooks(ExportBooksModel model)
        {                                                
            if (model.IsXml)
            {                
                return File(ExportHelper.ExportBooks(model,DataContext), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now + "-books.xml");
            }
            else
            {               
                return File(ExportHelper.ExportBooks(model,DataContext), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now + "-books.txt");
            }
        }

        [HttpPost]
        public FileResult TryExportJournals(ExportJournalsModel model)
        {
            if (model.IsXml)
            {
                return File(ExportHelper.ExportJournals(model, DataContext), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now + "-journals.xml");
            }
            else
            {
                return File(ExportHelper.ExportJournals(model, DataContext), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now + "-journals.txt");
            }
        }

        [HttpPost]
        public FileResult TryExportNewspapers(ExportNewspapersModel model)
        {
            if (model.IsXml)
            {
                return File(ExportHelper.ExportNewspapers(model, DataContext), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now + "-newspapers.xml");
            }
            else
            {
                return File(ExportHelper.ExportNewspapers(model, DataContext), System.Net.Mime.MediaTypeNames.Application.Octet,
                    "exp-" + DateTime.Now + "-newspapers.txt");
            }
        }
    }
}