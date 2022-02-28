
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace LawyerServices.Services.Data
{
    public interface IImageService
    {
        public  Task<string> UploadImage(IFormFile upload);

        public MemoryStream GenerateCircle(string firstName, string lastName);

        public string AddFolderAndImage(string names);

        public Task UserImageUploadAsync(InputFileChangeEventArgs e);
    }
}
