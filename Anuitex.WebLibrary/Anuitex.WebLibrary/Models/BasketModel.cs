using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models
{
    public class BasketModel : BaseModel
    {                
        public List<BookModel> BookProducts { get; set; }
        public List<JournalModel> JournalProducts { get; set; }
        public List<NewspaperModel> NewspaperProducts { get; set; }

        public int OrderId { get; set; }
        public double SumPrice { get; set; }
        public double TotalProductsCount => BookProducts.Count + JournalProducts.Count + NewspaperProducts.Count;

        public BasketModel()
        {
            BookProducts = new List<BookModel>();
            JournalProducts = new List<JournalModel>();
            NewspaperProducts = new List<NewspaperModel>();
        }
    }
}