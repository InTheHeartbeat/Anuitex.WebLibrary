using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data;

namespace Anuitex.WebLibrary.Models
{
    public class BooksModel : BaseModel
    {
        public List<Book> Books { get; set; }
    }
}