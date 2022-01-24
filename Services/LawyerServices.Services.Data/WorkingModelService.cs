using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class WorkingModelService : IWorkingModelService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<WorkingTime> workingRepository;

        public WorkingModelService(IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<WorkingTime> workingRepository)
        {
            this.companyRepository = companyRepository;
            this.workingRepository = workingRepository;
        }

        public async Task CreateInitialWorkingModel(string companyId)
        {
            var company = this.companyRepository.All().FirstOrDefault(c => c.Id == companyId);
            if (company == null)
            {
                return;
            }
            var workingTime = new WorkingTime()
            {
                Name = company.Names,
                IsActiv = true,
            };
            await this.workingRepository.AddAsync(workingTime);
            this.workingRepository.SaveChangesAsync();
           
        }
    }
}
