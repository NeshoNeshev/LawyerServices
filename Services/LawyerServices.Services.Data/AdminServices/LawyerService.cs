﻿using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.DateModels;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LawyerServices.Common;

namespace LawyerServices.Services.Data.AdminServices
{
    public class LawyerService : ILawyerService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<WorkingTime> workingRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<LawFirm> firmRepository;
        private readonly IImageService imageService;
        private readonly IRequestsService requestsService;


        public LawyerService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Town> townRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<WorkingTime> workingRepository,
            IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository,
            IImageService imageService,
            IRequestsService requestsService,
            IDeletableEntityRepository<LawFirm> firmRepository)
        {
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
            this.userRepository = userRepository;
            this.workingRepository = workingRepository;
            this.workingTimeExceptionRepository = workingTimeExceptionRepository;
            this.imageService = imageService;
            this.requestsService = requestsService;
            this.firmRepository = firmRepository;
        }
        public async Task<bool> IsOwner(string lawyerId)
        {
            var isOwner = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.IsOwner).FirstOrDefaultAsync();
            if (isOwner)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public async Task<string> CreateLawyerAsync(CreateLawyerModel lawyerModel)
        {
            var town = await this.townRepository.All().FirstOrDefaultAsync(t => t.Name == lawyerModel.TownName);

            var workingTime = new WorkingTime()
            {
                Id = Guid.NewGuid().ToString(),
                Name = lawyerModel.Email,
                IsActiv = true,
            };

            var imgUrl = this.imageService.AddFolderAndImage(lawyerModel.Names);

            var company = new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Names = lawyerModel.Names,
                OfficeName = lawyerModel.OfficeName,
                TownId = town.Id,
                Profession = lawyerModel.Role,
                Address = lawyerModel.AddressLocation,
                WorkingTimeId = workingTime.Id,
                ImgUrl = imgUrl,
                PhoneNumbers = lawyerModel.PhoneNumber,
                IsPublicPhoneNuber = lawyerModel.IsPublicPhoneNuber,
                RequestId = lawyerModel.RequestId,
                LicenceDate = lawyerModel.LicenceDate,
                Jurisdiction = lawyerModel.Jurisdiction,
                LastChecked = DateTime.Now.ToString("dd:MM:yyyy"),
                HeaderText = lawyerModel.HeaderText,
                AboutText = lawyerModel.AboutText,
                PhoneVerification = lawyerModel.PhoneVerification,
                Languages = AddLanguages(lawyerModel.Languages),
                ExpirationDate = lawyerModel.ExpirationDate,
               
            };
           
            await this.workingRepository.AddAsync(workingTime);
            await this.workingRepository.SaveChangesAsync();         

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
            await this.requestsService.SetIsApprovedAsync(lawyerModel.RequestId);

            return company.Id;

        }
        public async Task<string> EditImageAsync(byte[] bytes, string userId, string extension)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            var url = "";
            if (company != null)
            {
                if (company.ImgUrl != null)
                {
                    DeleteImage(company.ImgUrl);
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
                company.ImgUrl = imgUrl;

                this.companyRepository.Update(company);
                await this.companyRepository.SaveChangesAsync();

                url = imgUrl;
                return url;
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
        public async Task<bool> ExistingLawyerByPhoneAsync(string phoneNumber)
        {

            var exist = await this.userRepository.All().AnyAsync(p => p.PhoneNumber == phoneNumber);

            if (exist)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> ExistingLawyerByEmail(string email)
        {
            var exist = await this.userRepository.All().AnyAsync(p => p.Email == email);
            if (exist)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllLawyers<T>()
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer"));
           

            return await query.To<T>().ToListAsync();
        }
        public IEnumerable<T> GetAllLawyersByAdministrator<T>(int? count = null)
        {
            IQueryable<Company> query = this.companyRepository.AllWithDeleted().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).OrderBy(x=>x.ExpirationDate);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
        //Todo : implement categorySearch
        public IEnumerable<T> SearchAllLawyersByTownAndCategory<T>(string townId)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).Where(l => l.TownId == townId);

            return query.To<T>().ToList();
        }
        public async Task<T> GetLawyerAsync<T>(string Id)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Id == Id);
            
            return await query.To<T>().FirstOrDefaultAsync();
        }
        public async Task<LawyerListItem> GetLawyerByIdAsync(string lawyerId)
        {

            //var workingTime = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.WorkingTimeExceptions).FirstOrDefault();

            var lawyer = await this.companyRepository.All()
                .Where(x => x.StopAccount == false &&
                x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer") &&
                x.Id == lawyerId)
                .To<LawyerListItem>().FirstOrDefaultAsync();
            lawyer.WorkingTime.WorkingTimeExceptions = lawyer.WorkingTime.WorkingTimeExceptions.Where(
                x => x.StarFrom >= DateTime.UtcNow &&x.AppointmentType == GlobalConstants.Client && x.IsRequested == false && x.IsCanceled == false);


            return lawyer;
        }
        public bool ExistingLawyerById(string lawyerId)
        {
            var exist = this.companyRepository.All().Any(x => x.Id == lawyerId);
            if (!exist)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task UpdateLawyerImageAsync(string userId, string imgPath)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            if (company != null)
            {

                company.ImgUrl = imgPath;

                this.companyRepository.Update(company);
                await this.companyRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Image not exist");
            }

        }

        public async Task<AppointmentViewModel> GetLawyerWorkingTimeExteption(string appointmentId)
        {
            var appointment = await this.workingTimeExceptionRepository.All().Where(x => x.Id == appointmentId).To<AppointmentViewModel>().FirstOrDefaultAsync();

            return appointment;
        }
        public async Task<string> AddLawyerToLawFirmAsync(string companyId, string lawFirmId)
        {
            var lawyer = this.companyRepository.All().FirstOrDefault(x => x.Id == companyId);
            var lawfirm = this.firmRepository.All().FirstOrDefault(x => x.Id == lawFirmId);

            try
            {
                if (lawyer != null)
                {
                    lawyer.LawFirmId = lawfirm.Id;
                    this.companyRepository.Update(lawyer);
                    await this.companyRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Lawyer not created");
            }

            return lawfirm.Id;

        }
        public async Task EditLawyerByAdministratorAsync(EditLawyerModel inputModel)
        {
            var lawyer = this.companyRepository.All().FirstOrDefault(x => x.Id == inputModel.Id);

            try
            {
                lawyer.Address = inputModel.Address;
                lawyer.Languages = inputModel.Languages;
                lawyer.Names = inputModel.Names;
                lawyer.HeaderText = inputModel.HeaderText;
                lawyer.AboutText = inputModel.AboutText;
                lawyer.WebSite = inputModel.WebSite;
                lawyer.Jurisdiction = inputModel.Jurisdiction;
                lawyer.LicenceDate = inputModel.LicenceDate;
                lawyer.ExpirationDate = inputModel.ExpirationDate;
                this.companyRepository.Update(lawyer);
                await this.companyRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Lawyer is null");
            }

        }

        private string AddLanguages(List<string> languages)
        {
            var sb = new StringBuilder();
            foreach (var item in languages)
            {
                sb.AppendLine(item + " ");
            }

            return sb.ToString();
        }
        public async Task DeleteLawyer(string lawyerId)
        {
            var lawyer = this.companyRepository.All().FirstOrDefault(x => x.Id == lawyerId);
            try
            {
                if (lawyer != null)
                {
                    this.companyRepository.Delete(lawyer);
                    await this.companyRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new InvalidOperationException("lawyerId canot be null");
            }
        }
        public async Task RestoreAccount(string lawyerId)
        {
            var lawyer = this.companyRepository.AllWithDeleted().FirstOrDefault(x => x.Id == lawyerId);
            try
            {
                if (lawyer != null)
                {
                    this.companyRepository.Undelete(lawyer);
                    await this.companyRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new InvalidOperationException("lawyerId canot be null");
            }
        }
    }
}
