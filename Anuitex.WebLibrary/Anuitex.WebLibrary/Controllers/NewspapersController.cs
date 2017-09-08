using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;
using Anuitex.WebLibrary.Models;
using Anuitex.WebLibrary.ViewHelpers;

namespace Anuitex.WebLibrary.Controllers
{
    public class NewspapersController : BaseController
    {
        public ActionResult Index()
        {
            return View(new NewspapersModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("Index", "Newspapers", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                Newspapers = DataContext.Newspapers.Select(np => new NewspaperModel(np)).ToList(),
                CurrentNavSection = NavSection.Newspapers
            });
        }

        [HttpGet]
        public ActionResult AddNewspaper()
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToAction("Index", "Newspapers"); }

            return View(new AddNewspaperModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("AddNewspaper", "Newspapers", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                IsEdit = false,
                CurrentNavSection = NavSection.Newspapers
            });
        }

        [HttpPost]
        public ActionResult TryAddNewspaper(AddNewspaperModel model)
        {
            if (model == null) { return RedirectToAction("AddNewspaper", "Newspapers"); }
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToAction("Index", "Newspapers"); }

            DataContext.Newspapers.InsertOnSubmit(new Newspaper()
            {
                Title = model.Title,
                Periodicity = model.Periodicity,                
                Amount = model.Amount,
                Date = model.Date,
                Price = model.Price,
                PhotoId = model.PhotoId
            });
            DataContext.SubmitChanges();

            return RedirectToAction("Index", "Newspapers");
        }
        
        public ActionResult RemoveNewspaper(int id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToActionPermanent("Index"); }

            Newspaper newspaper = DataContext.Newspapers.FirstOrDefault(np => np.Id == id);

            if (newspaper == null)
            { return RedirectToActionPermanent("Index"); }

            DataContext.Newspapers.DeleteOnSubmit(newspaper);
            if (newspaper.Image != null)
            { DataContext.Images.DeleteOnSubmit(newspaper.Image); }
            DataContext.SubmitChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult EditNewspaper(int? id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            { return RedirectToActionPermanent("Index"); }

            Newspaper newspaper = DataContext.Newspapers.FirstOrDefault(np => np.Id == id);

            if (newspaper == null)
            { return RedirectToActionPermanent("Index"); }

            return View("AddNewspaper", new AddNewspaperModel()
            {
                BreadcrumbModel = new BreadcrumbModel(Url.Action("EditNewspaper", "Newspapers", null, Request.Url.Scheme)),
                CurrentUser = CurrentUser,
                IsEdit = true,
                Title = newspaper.Title,
                Periodicity = newspaper.Periodicity,                
                Date = newspaper.Date,
                Amount = newspaper.Amount,
                Price = newspaper.Price,
                PhotoId = newspaper.PhotoId,
                Id = newspaper.Id,
                PhotoPath = newspaper.Image?.Path,
                CurrentNavSection = NavSection.Newspapers
            });
        }

        [HttpPost]
        public ActionResult TryEditNewspaper(AddNewspaperModel model)
        {
            if (model != null)
            {
                Newspaper newspaper = DataContext.Newspapers.FirstOrDefault(np => np.Id == model.Id);
                newspaper.Title = model.Title;
                newspaper.Periodicity = model.Periodicity;                
                newspaper.Date = model.Date;
                newspaper.Price = model.Price;
                newspaper.Amount = model.Amount;
                newspaper.PhotoId = model.PhotoId;
                DataContext.SubmitChanges();

                return RedirectToAction("Index", "Newspapers");
            }
            return RedirectToAction("EditNewspaper", "Newspapers");
        }
    }
}