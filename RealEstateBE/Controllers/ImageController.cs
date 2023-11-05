﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;

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
        [HttpPost(Routes.uploadImages)]
        public async Task<IActionResult> UploadImage(IFormFileCollection formFiles, int propertyID)
        {
            int succesfulUpload = 0;
            try
            {
                string filePath = this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyID}";
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
            return (succesfulUpload - formFiles.Count != 0) ? BadRequest("Some files couldn't be uploaded.") : Ok("Files Uploaded successfully.");
        }

        [HttpGet(Routes.getImages)]
        public async Task<IActionResult> GetImages(int propertyID)
        {
            List<string> imageUrList = new();
            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            try
            {
                string filePath = GetFilePath(propertyID);
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
                            string imageUrl = hostUrl + $@"/Upload/Property{propertyID}/{fileName}";
                            imageUrList.Add(imageUrl);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(imageUrList);
        }
        [NonAction]
        private string GetFilePath(int propertyID)
        {
            return this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyID}";
        }
    }
}