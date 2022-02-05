using LawyerServices.Common.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class WorkingTimeExceptionService : IWorkingTimeExceptionService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;

        public WorkingTimeExceptionService(IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<WorkingTimeException> weRepository)
        {
            this.companyRepository = companyRepository;
            this.weRepository = weRepository;
        }

        public async Task SendRequestToLawyer(string lawyerId, string wteId , string userId)
        {
            var workingTimeException = this.weRepository.All().FirstOrDefault(ex => ex.Id == wteId);
            if (workingTimeException == null) return;
            workingTimeException.IsRequested = true;
            workingTimeException.UserId = lawyerId;
            this.weRepository.Update(workingTimeException);
            this.weRepository.SaveChangesAsync();




        }

        public async Task AcceptRequest()
        {

        }
    }
}
