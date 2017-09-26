using System.Collections.Generic;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models.IO.Export.Books
{
    public class ExportBooksModel : BaseModel
    {
        public List<ExportableBookModel> Books { get; set; }
        public bool IsXml { get; set; }
        public ExportBooksModel()
        {}     
    }
}