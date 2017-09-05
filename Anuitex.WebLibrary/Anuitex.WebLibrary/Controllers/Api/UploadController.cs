using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Anuitex.WebLibrary.Data;

namespace Anuitex.WebLibrary.Controllers.Api
{
    public class UploadController : BaseApiController
    {
        public async Task<IHttpActionResult> UploadBookPhoto()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();

            string relPath = "/Upload/Images/Books/";
            string root = System.Web.HttpContext.Current.Server.MapPath("~/"+relPath);
            int id = -1;

            await Request.Content.ReadAsMultipartAsync(provider);             
            foreach (HttpContent content in provider.Contents)
            {
                string fileName = content.Headers.ContentDisposition.FileName.Trim('\"');
                string customFileName = Guid.NewGuid().ToString() + new FileInfo(fileName).Extension;
                byte[] fileBytes = await content.ReadAsByteArrayAsync();
                
                using (FileStream fs = new FileStream(root + customFileName, FileMode.Create))
                {
                    await fs.WriteAsync(fileBytes, 0, fileBytes.Length);
                }
                DataContext.Images.InsertOnSubmit(new Image() {Path = relPath+customFileName});
                DataContext.SubmitChanges();
                id = DataContext.Images.FirstOrDefault(img => img.Path == relPath + customFileName).Id;
            }            

            return Ok(id);
        }
    }
}
