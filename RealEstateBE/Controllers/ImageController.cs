using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Entities;
using System.IO;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost(Routes.insert)]
        public async Task<IActionResult> UploadImage(int id)
        {
            var formFiles = Request.Form.Files;
            int succesfulUpload = 0;
            try
            {
                string filePath = this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{id}";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                foreach (var file in formFiles)
                {
                    string imagepath = filePath + $@"\\{file.FileName}";
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        succesfulUpload++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return (succesfulUpload - formFiles.Count != 0) ? BadRequest("Some files couldn't be uploaded.") : Ok($"{succesfulUpload} " + "Files Uploaded successfully");
        }

        [HttpGet(Routes.getById)]
        public IActionResult GetImages(int id)
        {
            List<Photo> photoList = new();
            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            try
            {
                string filePath = GetFilePath(id);
                if (Directory.Exists(filePath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string fileName = fileInfo.Name;
                        string imagePath = filePath + "\\" + fileName;
                        if (System.IO.File.Exists(imagePath))
                        {
                            string imageUrl = hostUrl + $@"/Upload/Property{id}/{fileName}";
                            var photo = new Photo()
                            {
                                name = fileName,
                                url = imageUrl,
                            };
                            photoList.Add(photo);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(photoList);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteImage(int propertyId,string imageName)
        {
            try
            {
                string filePath = this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyId}";
                string imagepath = filePath + $@"\\{imageName}";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                    if (!Directory.EnumerateFileSystemEntries(filePath).Any())
                    {
                        Directory.Delete(filePath);
                    }
                    return Ok(propertyId);
                }
            }
            catch (Exception)
            {
                throw;
            }
              return BadRequest("Such image does not exist.");
        }
        [NonAction]
        private string GetFilePath(int propertyID)
        {
            return this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyID}";
        }
    }
}
