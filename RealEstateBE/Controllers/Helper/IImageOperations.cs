using RealEstateEntities.Entities;

namespace RealEstateControllerLayer.Controllers.Helper
{
    public interface IImageOperations
    {
        public void UploadImages(string id, string category,IFormFileCollection formFiles, out int succesfulUpload);
        public IList<Photo> GetPhotos(string id, string category,string hostUrl, IList<Photo> photoList);
        public void DeleteImages(string id, string category, string imageName);
    }
}
