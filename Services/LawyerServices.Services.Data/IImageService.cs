using Microsoft.AspNetCore.Http;

namespace LawyerServices.Services.Data
{
    public interface IImageService
    {
        public  Task<bool> UploadImage(IFormFile upload);

        public MemoryStream GenerateCircle(string firstName, string lastName);

        public string AddFolderAndImage(string names);
    }
}
