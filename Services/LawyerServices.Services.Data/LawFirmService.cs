using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

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

        public async Task<string> CreateLawFirm(LawFirmInputModel model)
        {

            var town = this.townRepository.All().FirstOrDefault(x => x.Name.ToLower() == model.Town.ToLower());
            if (town == null) return null;

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

            return lawFirm.Id;
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

        public string GetIdByName(string name)
        { 
            var id = this.lawFirmrepository.All().Where(x=>x.Name.ToLower().Trim()  == name.ToLower().Trim()).Select(x=>x.Id).FirstOrDefault();

            return id;
        }

        public LawFirmViewModel GetLawFirm(string lawFirId)
        {
            var lawFirm = this.lawFirmrepository.All().To<LawFirmViewModel>().FirstOrDefault(x=>x.Id == lawFirId);

            return lawFirm;
        }
        public LawFirmViewModel FindLawFirmByName(string lawFirmName)
        {
            var lawFirm = this.lawFirmrepository.All().To<LawFirmViewModel>().FirstOrDefault(x => x.Name.ToLower() == lawFirmName.ToLower());

            return lawFirm;
        }
        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<LawFirm> query = this.lawFirmrepository.All();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task EditLawFirm(EditLawFirmAdministrationModel model)
        {
            var lawFirm = this.lawFirmrepository.All().FirstOrDefault(x=>x.Id == model.Id);
            if (lawFirm == null) return;
            try
            {
                lawFirm.About = model.About;
                lawFirm.Address = model.Address;
                lawFirm.FacebookUrl = model.FacebookUrl;
                lawFirm.Email = model.Email;
                lawFirm.Id = model.Id;
                lawFirm.LinkedinUrl = model.LinkedinUrl;
                lawFirm.PhoneNumber = model.PhoneNumber;
                lawFirm.WebSiteUrl = model.WebSiteUrl;
                lawFirm.Name = model.Name;
                this.lawFirmrepository.Update(lawFirm);
                this.lawFirmrepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Law firm not edited");
            }
        }
    }
}
