﻿//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using RealEstateBE.Controllers.Helper;
//using RealEstateEntities.Entities;
//using RealEstateBE.Security;
//using RealEstateService.Abstract;
//using System.IO;
//using System;

//namespace RealEstateBE.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ImageController : ControllerBase
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly IPropertyService _propertyService;
//        private readonly ISecurity _security;

//        public ImageController(IWebHostEnvironment webHostEnvironment,
//                               IPropertyService propertyService,
//                               ISecurity security)
//        {
//            _webHostEnvironment = webHostEnvironment;
//            _propertyService = propertyService;
//            _security= security;
//        }
//        [HttpPost(Routes.insert)]
//        [Authorize]
//        public IActionResult UploadImage(string id,string category)
//        {
//            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//            var property = _propertyService.GetProperty(id).Result;

//            if (token != null && property != null)
//            {
//                if  (!_security.IsAuthenticatedByToken(token, property.UserID))
//                {
//                    return Unauthorized();
//                }
//            }
//            var formFiles = Request.Form.Files;
//            int succesfulUpload = 0;
//            try
//            {
//                string filePath = this._webHostEnvironment.WebRootPath + $@"\\Upload\{category}{id}";
//                if (!Directory.Exists(filePath))
//                {
//                    Directory.CreateDirectory(filePath);
//                }
//                foreach (var file in formFiles)
//                {
//                    string imagepath = filePath + $@"\\{file.FileName}";
//                    if (System.IO.File.Exists(imagepath))
//                    {
//                        System.IO.File.Delete(imagepath);
//                    }
//                    using (FileStream stream = System.IO.File.Create(imagepath))
//                    {
//                        file.CopyTo(stream);
//                        succesfulUpload++;
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//            return (succesfulUpload - formFiles.Count != 0) ? BadRequest("Some files couldn't be uploaded.") : Ok($"{succesfulUpload} " + "Files Uploaded successfully");
//        }

//        [HttpGet(Routes.getById)]
//        public IActionResult GetImages(int id,string category)
//        {
//            List<Photo> photoList = new();
//            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
//            try
//            {
//                string filePath = GetFilePath(id);
//                if (Directory.Exists(filePath))
//                {
//                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
//                    FileInfo[] fileInfos = directoryInfo.GetFiles();
//                    foreach (FileInfo fileInfo in fileInfos)
//                    {
//                        string fileName = fileInfo.Name;
//                        string imagePath = filePath + "\\" + fileName;
//                        if (System.IO.File.Exists(imagePath))
//                        {
//                            string imageUrl = hostUrl + $@"/Upload/{category}{id}/{fileName}";
//                            var photo = new Photo()
//                            {
//                                name = fileName,
//                                url = imageUrl,
//                            };
//                            photoList.Add(photo);
//                        }
//                    }
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//            return Ok(photoList);
//        }

//        [HttpDelete("delete")]
//        [Authorize]
//        public IActionResult DeleteImage(string propertyGUID,string category,string imageName)
//        {
//            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//            var property = _propertyService.GetProperty(propertyGUID).Result;

//            if (token != null && property != null)
//            {
//                if (!_security.IsAuthenticatedByToken(token, property.UserID))
//                {
//                    return Unauthorized();
//                }
//            }

//            try
//            {
//                string imagepath = GetFilePath(propertyGUID, category) + $@"\\{imageName}";
//                if (System.IO.File.Exists(imagepath))
//                {
//                    System.IO.File.Delete(imagepath);
//                    if (!Directory.EnumerateFileSystemEntries(filePath).Any())
//                    {
//                        Directory.Delete(filePath);
//                    }
//                    return Ok(propertyId);
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//              return BadRequest("Such image does not exist.");
//        }
//        [NonAction]
//        private string GetFilePath(int propertyID)
//        {
//            return this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyID}";
//        }
//    }
//}
