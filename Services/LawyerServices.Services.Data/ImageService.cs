
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;

namespace LawyerServices.Services.Data
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private List<string> _BackgroundColours = new List<string> { "339966", "3366CC", "CC33FF", "FF5050" };

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile upload)
        {
            var path = string.Empty;
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
                path= $"/images/{fileName}.png";
                return path;
            }
            return path;
        }
        public MemoryStream GenerateCircle(string firstName, string lastName)
        {


            var avatarString = string.Format("{0}{1}", firstName[0], lastName[0]).ToUpper();

            var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
            var bgColour = _BackgroundColours[randomIndex];

            var bmp = new Bitmap(192, 192);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            var font = new Font("Arial", 72, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(bmp);

            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            using (Brush b = new SolidBrush(ColorTranslator.FromHtml("#" + bgColour)))
            {
                graphics.FillEllipse(b, new Rectangle(0, 0, 192, 192));
            }
            graphics.DrawString(avatarString, font, new SolidBrush(Color.WhiteSmoke), 95, 100, sf);
            graphics.Flush();

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            return ms;
        }
        //Todo : change image path 
        public string AddFolderAndImage(string names)
        {

            var namesArrey = names.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            Image returnImage = Image.FromStream(GenerateCircle(namesArrey[0], namesArrey[1]));
            returnImage.Save(filePath + $"/{names}.png");

            var path = $"/images/{names}.png";
            return path;
        }
        public string AddFolderAndImage(string firstName, string lastName)
        {

           
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            Image returnImage = Image.FromStream(GenerateCircle(firstName, lastName));
            returnImage.Save(filePath + $"/{firstName}{lastName}.png");
  
            var path = $"/images/{firstName}{lastName}.png";
            return path;
        }

        public async Task UserImageUploadAsync(InputFileChangeEventArgs e)
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/images", e.File.Name);
            await using FileStream fs = new(path, FileMode.Create);
            await e.File.OpenReadStream().CopyToAsync(fs);
        }
    }
}
