using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anuitex.WebLibrary.Models.IO.Export.Journals
{
    public class ExportJournalsModel : BaseModel
    {
        public List<ExportableJournalModel> Journals { get; set; }
        public bool IsXml { get; set; }

        public ExportJournalsModel()
        {
            
        }
    }
}