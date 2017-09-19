using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models.IO.Export.Newspapers
{
    [Serializable]
    public class ExportableNewspaperModel : NewspaperModel
    {
        public bool Selected { get; set; }
        public ExportableNewspaperModel(Newspaper baseNewspaper) : base(baseNewspaper)
        {
        }

        public ExportableNewspaperModel()
        {
            
        }
    }
}