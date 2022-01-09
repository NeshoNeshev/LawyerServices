using Microsoft.AspNetCore.Http;

namespace LawyerServices.Services.Data
{
    public interface IImageService
    {
        public  Task<bool> UploadImage(IFormFile upload);
    }
}
