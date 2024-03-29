﻿using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data
{
    public class LawFirmService : ILawFirmService
    {
        private readonly IDeletableEntityRepository<LawFirm> lawFirmrepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<Company> companyrepository;
        private readonly IDeletableEntityRepository<Moderator> moderatorRepository;
        private readonly IImageService imageService;
        public LawFirmService(IDeletableEntityRepository<LawFirm> lawFirmrepository, IDeletableEntityRepository<Town> townRepository, IImageService imageService, IDeletableEntityRepository<Company> companyrepository, IDeletableEntityRepository<Moderator> moderatorRepository)
        {
            this.lawFirmrepository = lawFirmrepository;
            this.townRepository = townRepository;
            this.imageService = imageService;
            this.companyrepository = companyrepository;
            this.moderatorRepository = moderatorRepository;
        }

        public async Task<string> CreateLawFirmAsync(LawFirmInputModel model)
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
                PhoneNumbers = model.PhoneNumber,
                IsPublicPhoneNuber = model.IsPublicPhone,
                About = model.About,
                Email = model.Email,
                PhoneVerification = model.PhoneVerification,
                ImgUrl = imgUrl,
            };

            await this.lawFirmrepository.AddAsync(lawFirm);
            await this.lawFirmrepository.SaveChangesAsync();

            return lawFirm.Id;
        }
        public async Task EditLawFirmImageAsync(byte[] bytes, string lawFirmId, string extension)
        {
            var lawFirm = this.lawFirmrepository.All().FirstOrDefault(x=>x.Id == lawFirmId);
            if (lawFirm != null)
            {
                if (lawFirm.ImgUrl != null)
                {
                    DeleteImage(lawFirm.ImgUrl);
                }

                var imageName = Guid.NewGuid().ToString();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", $"{imageName}{extension}");

                using (var ms = new MemoryStream(bytes))
                {

                    using var fs = new FileStream(filePath, FileMode.Create);

                    ms.WriteTo(fs);
                }
                var imgUrl = $"/images/{imageName}{extension}";
                lawFirm.ImgUrl = imgUrl;

                this.lawFirmrepository.Update(lawFirm);
                await this.lawFirmrepository.SaveChangesAsync();
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
        public void CreateModerator(ModeratorInputModel model)
        {
            
        }
        public async Task<LawFirmViewModel> GetLawFirm(string lawFirId)
        {
            var lawFirm = await this.lawFirmrepository.All().To<LawFirmViewModel>().FirstOrDefaultAsync(x=>x.Id == lawFirId);

            return lawFirm;
        }
        public async Task<string> GetLawFirmIdAsync(string lawyerId)
        {
            var lawFirmId = await this.companyrepository.All().Where(x => x.Id == lawyerId).Select(x => x.LawFirmId).FirstOrDefaultAsync();

            return lawFirmId;
        }
        public async Task<LawFirmViewModel> GetLawFirmByLawyerId(string moderatorId)
        {
            var lawfirm = await this.moderatorRepository.All().Where(x=>x.Id == moderatorId).Select(x => x.LawFirm).To<LawFirmViewModel>().FirstOrDefaultAsync();

            return lawfirm;
        }
        public LawFirmViewModel FindLawFirmByName(string lawFirmName)
        {
            var lawFirm = this.lawFirmrepository.All().To<LawFirmViewModel>().FirstOrDefault(x => x.Name.ToLower() == lawFirmName.ToLower());

            return lawFirm;
        }
        public  async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<LawFirm> query = this.lawFirmrepository.All();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }
            var result = await query.To<T>().ToListAsync();
            return result;
        }

        public async Task EditLawFirmAsync(EditLawFirmAdministrationModel model)
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
                lawFirm.PhoneNumbers = model.PhoneNumber;
                lawFirm.WebSiteUrl = model.WebSiteUrl;
                lawFirm.Name = model.Name;
                this.lawFirmrepository.Update(lawFirm);
                await this.lawFirmrepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Law firm not edited");
            }
        }
    }
}
