using System.Collections.Generic;
using Anuitex.WebLibrary.Models.IO.Export.Newspapers;

namespace Anuitex.WebLibrary.Models.IO.Import.Newspapers
{
    public class ImportNewspapersResultModel : BaseModel
    {
        public List<ExportableNewspaperModel> Newspapers { get; set; }        
        public ImportNewspapersResultModel()
        {
            
        }
    }
}