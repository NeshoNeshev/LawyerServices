using Microsoft.AspNetCore.Http;

namespace LawyerServices.Services.Data
{
    public class ImageService : IImageService
    {
        public async Task<bool> UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                //postNin.ImagePath = filePath;
                //_context.PostNins.Add(postNin);
                //_context.SaveChanges();
                using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(fileSrteam);
                }
                return true;
            }
            return false;
        }
    }
}
