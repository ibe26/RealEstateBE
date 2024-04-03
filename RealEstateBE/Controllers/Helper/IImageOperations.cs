using RealEstateEntities.Entities;

namespace RealEstateControllerLayer.Controllers.Helper
{
    public interface IImageOperations
    {
        public void UploadImages(string propertyGUID, IFormFileCollection formFiles, out int succesfulUpload);
        public IList<Photo> GetPhotos(string propertyGUID, string hostUrl, out IList<Photo> photoList);
    }
}
