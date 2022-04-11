using LaweyrServices.Web.Shared.LawFirmModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class LawFirmService : ILawFirmService
    {
        private readonly IDeletableEntityRepository<LawFirm> lawFirmrepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IImageService imageService;

        public LawFirmService(IDeletableEntityRepository<LawFirm> lawFirmrepository, IDeletableEntityRepository<Town> townRepository, IImageService imageService)
        {
            this.lawFirmrepository = lawFirmrepository;
            this.townRepository = townRepository;
            this.imageService = imageService;
        }

        public async Task CreateLawFirm(LawFirmInputModel model)
        {

            var town = this.townRepository.All().FirstOrDefault(x => x.Name.ToLower() == model.Town.ToLower());
            if (town == null) return;

            var imgUrl = this.imageService.AddFolderAndImage(model.Name);

            var lawFirm = new LawFirm()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Town = town,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                About = model.About,
                Email = model.Email,
                PhoneVerification = model.PhoneVerification,
                ImgUrl = imgUrl,
            };

            await this.lawFirmrepository.AddAsync(lawFirm);
            this.lawFirmrepository.SaveChangesAsync();
        }
        public async Task EditLawFirmImage(byte[] bytes, string lawFirmId, string extension)
        {
            var lawFirm = this.lawFirmrepository.All().FirstOrDefault(x=>x.Id == lawFirmId);
            if (lawFirm != null)
            {
                if (lawFirm.ImgUrl != null)
                {
                    DeleteImage(lawFirm.ImgUrl);
                }

                var imageName = Guid.NewGuid().ToString();
                //get aweyter get result
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", $"{imageName}{extension}");

                using (var ms = new MemoryStream(bytes))
                {

                    using var fs = new FileStream(filePath, FileMode.Create);

                    ms.WriteTo(fs);
                }
                var imgUrl = $"/images/{imageName}{extension}";
                lawFirm.ImgUrl = imgUrl;

                this.lawFirmrepository.Update(lawFirm);
                this.lawFirmrepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Image not exist");
            }
        }
        private void DeleteImage(string umgUrl)
        {

            var imageName = umgUrl.Split("/", StringSplitOptions.RemoveEmptyEntries).ToArray();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", imageName[1]);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

        }
    }
}
