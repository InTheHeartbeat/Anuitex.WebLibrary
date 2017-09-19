using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models.IO.Export.Journals
{
    [Serializable]
    public class ExportableJournalModel : JournalModel
    {
        public bool Selected { get; set; }

        public ExportableJournalModel(Journal baseJournal) : base(baseJournal)
        {
        }

        public ExportableJournalModel()
        {
            
        }
    }
}