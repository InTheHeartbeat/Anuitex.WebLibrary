using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data.Models;
using Anuitex.WebLibrary.Models.IO.Export.Books;

namespace Anuitex.WebLibrary.Models.IO.Import.Books
{
    public class ImportBooksResultModel : BaseModel
    {
        public List<ExportableBookModel> Books { get; set; }
        public ImportBooksResultModel()
        {
            
        }
    }
}