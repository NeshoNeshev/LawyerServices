﻿using LawyerServices.Common.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class LawyerScheduleService : ILawyerScheduleService
    {

        private readonly IDeletableEntityRepository<WorkingTime> workingTimeRepository;
        private readonly IDeletableEntityRepository<Company> companyRepository;
        public LawyerScheduleService(IDeletableEntityRepository<WorkingTime> workingTimeRepository, IDeletableEntityRepository<Company> companyRepository)
        {
            this.workingTimeRepository = workingTimeRepository;
            this.companyRepository = companyRepository;
        }

        public async Task CreateScheduleException(WorkingTimeExceptionInputModel model)
        {
            var workingTime = this.workingTimeRepository.All().FirstOrDefault(w => w.Id == GetWorkingTimeId(model.companyId));

            if (workingTime == null) return;
           
            var workingException = new WorkingTimeException()
            {
                Date = model.Date.Date,
                StarFrom = model.StartFrom.Date,
            
            };
        }

        private string GetWorkingTimeId(string companyId)
        {
            var workingTimeId = this.companyRepository.All().Where(c => c.Id == companyId).Select(x => x.WorkingTimeId).FirstOrDefault();
            if (workingTimeId == null) return null;
            return workingTimeId;
        }
    }
}