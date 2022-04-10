using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

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
