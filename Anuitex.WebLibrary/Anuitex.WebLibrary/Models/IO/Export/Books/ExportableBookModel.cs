using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models.IO.Export.Books
{
    [Serializable]
    public class ExportableBookModel : BookModel
    {
        public bool Selected { get; set; }

        public ExportableBookModel(Book baseBook) : base(baseBook)
        {}

        public ExportableBookModel()
        {}
    }
}