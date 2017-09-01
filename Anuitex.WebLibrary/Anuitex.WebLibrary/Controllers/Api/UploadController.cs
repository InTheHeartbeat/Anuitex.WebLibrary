using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Anuitex.WebLibrary.Data;

namespace Anuitex.WebLibrary.Controllers.Api
{
    public class UploadController : ApiController
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
                byte[] fileBytes = await content.ReadAsByteArrayAsync();
                
                using (FileStream fs = new FileStream(root + fileName, FileMode.Create))
                {
                    await fs.WriteAsync(fileBytes, 0, fileBytes.Length);
                }
                DataContext.Context.LibraryDataContext.Images.InsertOnSubmit(new Image() {Path = relPath+fileName});
                DataContext.Context.LibraryDataContext.SubmitChanges();
                id = DataContext.Context.LibraryDataContext.Images.FirstOrDefault(img => img.Path == relPath + fileName).Id;
            }            

            return Ok(id);
        }
    }
}
