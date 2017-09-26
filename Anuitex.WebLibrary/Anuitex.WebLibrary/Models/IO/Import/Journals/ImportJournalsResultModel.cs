using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Models.IO.Export.Journals;

namespace Anuitex.WebLibrary.Models.IO.Import.Journals
{
    public class ImportJournalsResultModel : BaseModel
    {
        public List<ExportableJournalModel> Journals { get; set; }

        public ImportJournalsResultModel()
        {}
    }
}