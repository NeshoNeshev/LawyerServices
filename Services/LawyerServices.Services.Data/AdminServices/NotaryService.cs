using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

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
        public IEnumerable<T> GetAllNotary<T>(int? count = null)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Notary")).OrderBy(x=>x.Names);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
        public async Task EditNotaryByAdministrator(EditNotaryModel inputModel)
        {
            var notary = this.companyRepository.All().FirstOrDefault(x => x.Id == inputModel.Id);

            try
            {
                notary.Names = inputModel.Names;
                notary.Address = inputModel.AddressLocation;
                notary.OfficeName = inputModel.OfficeName;
                notary.WebSite = inputModel.WebSite;
                notary.AboutText = inputModel.About;

                this.companyRepository.Update(notary);
                this.companyRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Notary is null");
            }

        }
        public async Task<string> CreateNotary(CreateNotaryModel notaryModel)
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

            };

            await this.workingRepository.AddAsync(workingTime);
            this.workingRepository.SaveChangesAsync();

            await this.companyRepository.AddAsync(company);
            this.companyRepository.SaveChangesAsync();
            //await this.requestsService.SetIsApproved(lawyerModel.RequestId);
            return company.Id;

            //Todo: password

        }
    }

}
