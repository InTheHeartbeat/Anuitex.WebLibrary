using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models
{
    public class JournalsModel : BaseModel
    {
        public List<JournalModel> Journals { get; set; }
    }
}