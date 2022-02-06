using LawyerServices.Services.Mapping;
using LawyerServices.Common.WorkingTimeModels;
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
            workingTimeException.UserId = userId;
            this.weRepository.Update(workingTimeException);
            this.weRepository.SaveChangesAsync();




        }
        public int GetRequstsCount(string lawyerId)
        {
            if (lawyerId == null) return 0;
            var workingTimeExceptions = this.companyRepository.All().Where(l => l.Id == lawyerId).Select(x => x.WorkingTime).Select(x => x.WorkingTimeException.Where(x => x.IsRequested == true)).FirstOrDefault();
            var count = workingTimeExceptions?.Count();
            if (count.HasValue)
            {
                return count.Value;
            }
            return 0;
        }
        public async Task<WorkingTimeExceptionBookingModel> AcceptRequest(string wteId)
        {
            var wtexc = this.weRepository.All().Where(w => w.Id == wteId).To<WorkingTimeExceptionBookingModel>().FirstOrDefault();

            return wtexc;
        }
    }
}