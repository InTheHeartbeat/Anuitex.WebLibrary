using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Models.IO.Export
{
    public class ExportNewspapersModel :BaseModel
    {
        public List<NewspaperModel> Newspapers { get; set; }
    }
}