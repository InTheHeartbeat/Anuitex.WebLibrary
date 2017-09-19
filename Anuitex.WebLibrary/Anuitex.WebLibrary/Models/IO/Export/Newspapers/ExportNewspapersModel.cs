using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anuitex.WebLibrary.Models.IO.Export.Newspapers
{
    [Serializable]
    public class ExportNewspapersModel : BaseModel
    {
        public List<ExportableNewspaperModel> Newspapers { get; set; }
        public bool IsXml { get; set; }

        public ExportNewspapersModel()
        {
            
        }
    }
}