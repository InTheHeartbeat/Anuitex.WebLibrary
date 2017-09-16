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
    public class SellController : BaseController
    {

        public ActionResult Basket()
        {
            BasketModel model = new BasketModel();

            if (CurrentUser != null)
            {
                AccountOrder order = CurrentUser.AccountOrders.LastOrDefault(ord => !ord.Completed);

                if (order != null)
                {
                    model.SumPrice = order.Sum;
                    model.OrderId = order.Id;
                    foreach (AccountOrderRecord record in order.AccountOrderRecords)
                    {
                        if (record.ProductType == typeof(Book).Name)
                        {
                            model.BookProducts.Add(
                                new BookModel(
                                    DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)));
                        }
                        if (record.ProductType == typeof(Journal).Name)
                        {
                            model.JournalProducts.Add(
                                new JournalModel(
                                    DataContext.Journals.FirstOrDefault(
                                        journal => journal.Id == record.ProductId)));
                        }
                        if (record.ProductType == typeof(Newspaper).Name)
                        {
                            model.NewspaperProducts.Add(
                                new NewspaperModel(
                                    DataContext.Newspapers.FirstOrDefault(
                                        newspaper => newspaper.Id == record.ProductId)));
                        }
                    }
                }
            }


            if (CurrentVisitor != null)
            {
                VisitorOrder order = CurrentVisitor.VisitorOrders.LastOrDefault(ord => !ord.Completed);

                if (order != null)
                {
                    model.SumPrice = order.Sum;
                    model.OrderId = order.Id;
                    foreach (VisitorOrderRecord record in order.VisitorOrderRecords)
                    {
                        if (record.ProductType == typeof(Book).Name)
                        {
                            model.BookProducts.Add(
                                new BookModel(
                                    DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)));
                        }
                        if (record.ProductType == typeof(Journal).Name)
                        {
                            model.JournalProducts.Add(
                                new JournalModel(
                                    DataContext.Journals.FirstOrDefault(
                                        journal => journal.Id == record.ProductId)));
                        }
                        if (record.ProductType == typeof(Newspaper).Name)
                        {
                            model.NewspaperProducts.Add(
                                new NewspaperModel(
                                    DataContext.Newspapers.FirstOrDefault(
                                        newspaper => newspaper.Id == record.ProductId)));
                        }
                    }
                }
            }

            model.BreadcrumbModel = new BreadcrumbModel(Url.Action("Basket", "Sell", null, Request.Url.Scheme));
            model.CurrentUser = CurrentUser;
            model.CurrentNavSection = NavSection.Basket;
            return View("Basket", model);
        }


        public ActionResult SellProduct(int code, string type, int count)
        {
            SellIsAccount(code,type,count);
            SellIsVisitor(code,type,count);
            
            return RedirectToAction("Basket");
        }

        public ActionResult AcceptSellOrder(int orderId)
        {
            if (CurrentUser != null)
            {
                AccountOrder order = DataContext.AccountOrders.FirstOrDefault(ord => ord.Id == orderId);
                if (order != null)
                {
                    foreach (AccountOrderRecord record in order.AccountOrderRecords)
                    {
                        if (record.ProductType == typeof(Book).Name)
                        {
                            DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId).Amount -=
                                record.Count;
                        }
                        if (record.ProductType == typeof(Journal).Name)
                        {
                            DataContext.Journals.FirstOrDefault(jor => jor.Id == record.ProductId).Amount -=
                                record.Count;
                        }
                        if (record.ProductType == typeof(Newspaper).Name)
                        {
                            DataContext.Newspapers.FirstOrDefault(np => np.Id == record.ProductId ).Amount -= record.Count;
                        }
                    }

                    order.Completed = true;                    

                    DataContext.SubmitChanges();
                }
            }

            if (CurrentVisitor != null)
            {
                VisitorOrder order = DataContext.VisitorOrders.FirstOrDefault(ord => ord.Id == orderId);
                if (order != null)
                {
                    foreach (VisitorOrderRecord record in order.VisitorOrderRecords)
                    {
                        if (record.ProductType == typeof(Book).Name)
                        {
                            DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId).Amount -=
                                record.Count;
                        }
                        if (record.ProductType == typeof(Journal).Name)
                        {
                            DataContext.Journals.FirstOrDefault(jor => jor.Id == record.ProductId).Amount -=
                                record.Count;
                        }
                        if (record.ProductType == typeof(Newspaper).Name)
                        {
                            DataContext.Newspapers.FirstOrDefault(np => np.Id == record.ProductId).Amount -= record.Count;
                        }
                    }

                    order.Completed = true;

                    DataContext.SubmitChanges();
                }
            }

            return RedirectToAction("Basket");
        }

        public ActionResult RemoveProductFromBasket(int productId, string productType)
        {
            AccountOrder currentAccountOrder = CurrentUser?.AccountOrders.LastOrDefault(ord => !ord.Completed);
            AccountOrderRecord accountOrderRecord = currentAccountOrder?.AccountOrderRecords.FirstOrDefault(
                rec => rec.ProductId == productId && rec.ProductType == productType);
            if (accountOrderRecord != null)
            {                
                DataContext.AccountOrderRecords.DeleteOnSubmit(accountOrderRecord);
                DataContext.SubmitChanges();
                CalcAccountOrderSum(currentAccountOrder, accountOrderRecord.Count);                
            }

            VisitorOrder currentVisitorOrder = CurrentVisitor?.VisitorOrders.LastOrDefault(ord => !ord.Completed);
            VisitorOrderRecord visitorOrderRecord =
                currentVisitorOrder?.VisitorOrderRecords.FirstOrDefault(
                    rec => rec.ProductId == productId && rec.ProductType == productType);
            if (visitorOrderRecord != null)
            {
                DataContext.VisitorOrderRecords.DeleteOnSubmit(visitorOrderRecord);
                DataContext.SubmitChanges();
                CalcVisitorOrderSum(currentVisitorOrder, visitorOrderRecord.Count);
            }           

            return RedirectToAction("Basket");
        }

        private void SellIsAccount(int code, string type, int count)
        {
            if (CurrentUser != null)
            {                
                AccountOrderRecord record = new AccountOrderRecord()
                {
                    Count = count,
                    ProductId = code,
                    ProductType = type,                    
                };

                AccountOrder order = CurrentUser.AccountOrders.LastOrDefault(ord => !ord.Completed);
                if (order != null)
                {
                    record.AccountOrder = order;
                    order.AccountOrderRecords.Add(record);
                }
                else
                {
                    order = new AccountOrder()
                    {
                        Account = CurrentUser,
                        AccountId = CurrentUser.Id,
                        Completed = false
                    };

                    record.AccountOrder = order;
                    order.AccountOrderRecords.Add(record);
                    DataContext.AccountOrders.InsertOnSubmit(order);

                    CurrentUser.AccountOrders.Add(order);
                }

                CalcAccountOrderSum(order, count);
               
                DataContext.SubmitChanges();
            }
        }

        private void CalcAccountOrderSum(AccountOrder order, int count)
        {
            if (order == null)
            {
                return;
            }
            order.Sum = 0;

            foreach (AccountOrderRecord record in order.AccountOrderRecords)
            {
                if (record.ProductType == typeof(Book).Name)
                {
                    order.Sum += (float) DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;

                }

                if (record.ProductType == typeof(Journal).Name)
                {
                    order.Sum += (float) DataContext.Journals
                                     .FirstOrDefault(journal => journal.Id == record.ProductId)
                                     .Price * count;
                }

                if (record.ProductType == typeof(Newspaper).Name)
                {
                    order.Sum += (float) DataContext.Newspapers.FirstOrDefault(np => np.Id == record.ProductId)
                                     .Price * count;
                }
            }
            DataContext.SubmitChanges();
        }

        private void SellIsVisitor(int code, string type, int count)
        {
            if (CurrentVisitor != null)
            {
                VisitorOrderRecord record = new VisitorOrderRecord()
                {
                    Count = count,
                    ProductId = code,
                    ProductType = type,
                };

                VisitorOrder order = CurrentVisitor.VisitorOrders.LastOrDefault(ord => !ord.Completed);
                if (order != null)
                {
                    record.VisitorOrder = order;            
                    order.VisitorOrderRecords.Add(record);
                }
                else
                {
                    order = new VisitorOrder()
                    {
                        Visitor = CurrentVisitor,
                        VisitorId = CurrentVisitor.Id,
                        Completed = false
                    };

                    record.VisitorOrder = order;
                    order.VisitorOrderRecords.Add(record);                    

                    CurrentVisitor.VisitorOrders.Add(order);                    
                }

                CalcVisitorOrderSum(order, count);

                DataContext.SubmitChanges();
            }
        }
        private void CalcVisitorOrderSum(VisitorOrder order, int count)
        {
            if (order == null) { return;}
            order.Sum = 0;

            foreach (VisitorOrderRecord record in order.VisitorOrderRecords)
            {
                if (record.ProductType == typeof(Book).Name)
                {
                    order.Sum += (float) DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;
                }

                if (record.ProductType == typeof(Journal).Name)
                {

                    order.Sum += (float) DataContext.Journals
                                     .FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;
                }

                if (record.ProductType == typeof(Newspaper).Name)
                {
                    order.Sum += (float) DataContext.Newspapers
                                     .FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;
                }
            }
            DataContext.SubmitChanges();
        }
    }
}