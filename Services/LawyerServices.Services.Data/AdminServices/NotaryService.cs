using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data.AdminServices
{
    public class NotaryService : INotaryService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<WorkingTime> workingRepository;

        private readonly IImageService imageService;
        public NotaryService(IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<Town> townRepository, IDeletableEntityRepository<WorkingTime> workingRepository, IImageService imageService)
        {
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
            this.workingRepository = workingRepository;
            this.imageService = imageService;
        }
        public async Task<IEnumerable<T>> GetAllNotary<T>(int? count = null)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x=>x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).OrderBy(x=>x.Names);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }
        public async Task<ICollection<T>> GetAllNotaryByTown<T>(string? townName)
        {
            IQueryable<Company> query;
            if (String.IsNullOrEmpty(townName))
            {
                 query = this.companyRepository.All().Where(x => x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).OrderBy(x => x.Names);
            }
            else
            {
                query = this.companyRepository.All().Where(x => x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).Where(x => x.Town.Name == townName).OrderBy(x => x.Names);
            }
       
           

            return await query.To<T>().ToListAsync();
        }
        public IEnumerable<T> GetAllNotaryByAdministrator<T>(int? count = null)
        {
            IQueryable<Company> query = this.companyRepository.AllWithDeleted().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).OrderBy(x => x.ExpirationDate);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
        public async Task EditNotaryByAdministratorAsync(EditNotaryModel inputModel)
        {
            var notary = this.companyRepository.All().FirstOrDefault(x => x.Id == inputModel.Id);

            try
            {
                notary.Names = inputModel.Names;
                notary.Address = inputModel.AddressLocation;
                notary.OfficeName = inputModel.OfficeName;
                notary.WebSite = inputModel.WebSite;
                notary.AboutText = inputModel.About;
                notary.WorkInSaturday = inputModel.WorkInSaturday;
                notary.WorkInSunday = inputModel.WorkInSunday;
                notary.OfficeEmails = inputModel.OfficeEmails;
                notary.PhoneNumbers = inputModel.OfficeNumbers;
                notary.IsReminderForComing = inputModel.IsReminderForComing;
                notary.IsSendSms = inputModel.IsSendSms;
                notary.ExpirationDate = inputModel.ExpirationDate;
                this.companyRepository.Update(notary);
                await this.companyRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Notary is null");
            }

        }
        public async Task<string> CreateNotaryAsync(CreateNotaryModel notaryModel)
        {
            var town = this.townRepository.All().FirstOrDefault(t => t.Name == notaryModel.TownName);

            var workingTime = new WorkingTime()
            {
                Id = Guid.NewGuid().ToString(),
                Name = notaryModel.Email,
                IsActiv = true,
            };


            var imgUrl = this.imageService.AddFolderAndImage(notaryModel.Names);

            var company = new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Names = notaryModel.Names,
                OfficeName = notaryModel.OfficeName,
                TownId = town.Id,
                Profession = notaryModel.Role,
                Address = notaryModel.AddressLocation,
                WorkingTimeId = workingTime.Id,
                ImgUrl = imgUrl,
                AboutText = notaryModel.About,
                WebSite = notaryModel.WebSite,
                PhoneVerification = notaryModel.PhoneVerification,
                WorkInSaturday = notaryModel.WorkInSaturday,
                WorkInSunday = notaryModel.WorkInSunday,
                OfficeEmails = notaryModel.OfficeEmails,
                PhoneNumbers = notaryModel.OfficeNumbers,
                ExpirationDate = notaryModel.ExpirationDate,
            };

            await this.workingRepository.AddAsync(workingTime);
            await this.workingRepository.SaveChangesAsync();

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
            //await this.requestsService.SetIsApproved(lawyerModel.RequestId);
            return company.Id;

            //Todo: password

        }

        public async Task<NotaryViewModel> GetNotaryById(string notaryId)
        { 
            var result = await this.companyRepository.All().Where(x=>x.StopAccount == false).Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).To<NotaryViewModel>().FirstOrDefaultAsync(x=>x.Id == notaryId);

            return result;
        }
        public async Task DeleteNotary(string notaryId)
        {
            var notary = this.companyRepository.All().FirstOrDefault(x => x.Id == notaryId);
            try
            {
                if (notary != null)
                {
                    this.companyRepository.Delete(notary);
                    await this.companyRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new InvalidOperationException("notaryId canot be null");
            }
        }
        public async Task RestoreAccount(string notaryId)
        {
            var notary = this.companyRepository.AllWithDeleted().FirstOrDefault(x => x.Id == notaryId);
            try
            {
                if (notary != null)
                {
                    this.companyRepository.Undelete(notary);
                    await this.companyRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new InvalidOperationException("notaryId canot be null");
            }
        }
    }
}
