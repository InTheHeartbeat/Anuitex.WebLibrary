using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class JournalsController : BaseController
    {        
        public ActionResult Index()
        {
            return View(new JournalsModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Index", "Journals", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                Journals = DataContext.Journals.Select(jour => new JournalModel(jour)).ToList(),
                CurrentNavSection = NavSection.Journals
            });
        }

        [HttpGet]
        public ActionResult AddJournal()
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToAction("Index", "Journals"); }

            return View(new AddJournalModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("AddJournal", "Journals", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                IsEdit = false,
                CurrentNavSection = NavSection.Journals
            });
        }

        [HttpPost]
        public ActionResult TryAddJournal(AddJournalModel model)
        {
            if (model == null) { return RedirectToAction("AddJournal", "Journals"); }
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToAction("Index", "Journals"); }

            DataContext.Journals.InsertOnSubmit(new Journal()
            {
                Title = model.Title,
                Periodicity = model.Periodicity,
                Subjects = model.Subjects,
                Amount = model.Amount,
                Date = model.Date,
                Price = model.Price,                
                PhotoId = model.PhotoId
            });
            DataContext.SubmitChanges();

            return RedirectToAction("Index", "Journals");
        }

        [Route("Journals/RemoveJournal/{id:int}")]        
        public ActionResult RemoveJournal(int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToActionPermanent("Index"); }

            Journal journal = DataContext.Journals.FirstOrDefault(jour => jour.Id == id);

            if (journal == null)
            { return RedirectToActionPermanent("Index"); }

            DataContext.Journals.DeleteOnSubmit(journal);            
            DataContext.SubmitChanges();

            return RedirectToAction("Index");
        }
       
        [Route("Journals/EditJournal/{id:int}")]
        public ActionResult EditJournal(int? id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToActionPermanent("Index"); }

            Journal journal = DataContext.Journals.FirstOrDefault(jour => jour.Id == id);

            if (journal == null)
            { return RedirectToActionPermanent("Index"); }

            return View("AddJournal", new AddJournalModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("EditJournal", "Journals", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                IsEdit = true,
                Title = journal.Title,
                Periodicity = journal.Periodicity,
                Subjects = journal.Subjects,                
                Date = journal.Date,
                Amount = journal.Amount,
                Price = journal.Price,
                PhotoId = journal.PhotoId,
                Id = journal.Id,
                PhotoPath = journal.Image?.Path,
                CurrentNavSection = NavSection.Journals
            });
        }

        [HttpPost]
        public ActionResult TryEditJournal(AddJournalModel model)
        {
            if (model != null)
            {
                Journal journal = DataContext.Journals.FirstOrDefault(jour => jour.Id == model.Id);
                journal.Title = model.Title;
                journal.Periodicity = model.Periodicity;
                journal.Subjects = model.Subjects;
                journal.Date = model.Date;                
                journal.Price = model.Price;
                journal.Amount = model.Amount;
                journal.PhotoId = model.PhotoId;
                DataContext.SubmitChanges();

                return RedirectToAction("Index", "Journals");
            }
            return RedirectToAction("EditJournal", "Journals");
        }
    }
}