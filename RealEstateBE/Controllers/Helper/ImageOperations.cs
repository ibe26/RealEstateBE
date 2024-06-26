﻿using Azure.Core;
using RealEstateEntities.Entities;
using System.IO;

namespace RealEstateControllerLayer.Controllers.Helper
{
    public class ImageOperations : IImageOperations
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageOperations(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IList<Photo> GetPhotos(string id, string category, string hostUrl, IList<Photo> photoList)
        {
            try
            {
                string filePath = GetFilePath(id, category);
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
                            string imageUrl = hostUrl + $@"/Upload/{category}{id}/{fileName}";
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
            return photoList;
        }

        public void UploadImages(string id, string category, IFormFileCollection formFiles, out int succesfulUpload)
        {
            try
            {
                succesfulUpload = 0;
                if (!Directory.Exists(GetFilePath(id, category)))
                {
                    Directory.CreateDirectory(GetFilePath(id, category));
                }
                foreach (var file in formFiles)
                {
                    string imagepath = GetFilePath(id, category) + $@"\\{file.FileName}";
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath);
                    }
                    using (FileStream stream = File.Create(imagepath))
                    {
                        file.CopyTo(stream);
                        succesfulUpload++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UploadImageSingle(string id, string category, IFormFile formFile)
        {
            string imagepath = GetFilePath(id, category);

            try
            {
                if (Directory.Exists(imagepath))
                {
                    Directory.Delete(imagepath, true);
                }
                Directory.CreateDirectory(imagepath);

                using (FileStream stream = File.Create(imagepath + $@"\\{formFile.FileName}"))
                {
                    formFile.CopyTo(stream);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteImages(string id, string category, string imageName)
        {
            try
            {
                string filePath = GetFilePath(id, category);
                string imagepath = filePath + $@"\\{imageName}";
                if (File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                    if (!Directory.EnumerateFileSystemEntries(filePath).Any())
                    {
                        Directory.Delete(filePath);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GetFilePath(string id, string category)
        {
            return _webHostEnvironment.WebRootPath + $@"\\Upload\\{category}{id}";
        }
    }
}
