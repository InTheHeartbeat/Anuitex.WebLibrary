using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models
{
    public class BooksModel : BaseModel
    {
        public List<BookModel> Books { get; set; }

    }
}