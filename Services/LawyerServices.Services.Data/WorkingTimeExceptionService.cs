﻿using LawyerServices.Services.Mapping;
using LawyerServices.Common.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class WorkingTimeExceptionService : IWorkingTimeExceptionService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;

        public WorkingTimeExceptionService(IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<WorkingTimeException> weRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyRepository;
            this.weRepository = weRepository;
            this.userRepository = userRepository;
        }

        public async Task SendRequestToLawyer(string lawyerId, string wteId, string userId)
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
        public async Task<WorkingTimeExceptionBookingModel> GetRequestById(string wteId)
        {
            //var exceptions = this.userRepository.All().Where(u => u.Id == wteId).
            //    Select(c => c.Company)
            //    .Select(w => w.WorkingTime)
            //    .Select(wte => wte.WorkingTimeException.Where(x=>x.IsRequested == true)).FirstOrDefault(); 
            var wtexc = this.weRepository.All().Where(w => w.Id == wteId).Where(x => x.IsRequested == true).To<WorkingTimeExceptionBookingModel>().FirstOrDefault();

            return wtexc;
        }
        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequsts(string userId)
        {
            var workingTimeId = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTimeId).FirstOrDefault();
            var exc = this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true).To<WorkingTimeExceptionBookingModel>().ToList();
            //var exceptions = this.userRepository.All().Where(u => u.Id == userId).
            //    Select(c => c.Company)
            //    .Select(w => w.WorkingTime)
            //    .Select(wte => wte.WorkingTimeException.Where(x => x.IsRequested == true)).To<WorkingTimeExceptionBookingModel>().ToList();

            return exc;
        }
        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequestsByDayOfWeek(string userId, DateTime search)
        {
            var workingTimeId = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTimeId).FirstOrDefault();
            if (search.Date == DateTime.UtcNow.Date)
            {
                return this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true).Where(x => x.Date.Date == search.Date).To<WorkingTimeExceptionBookingModel>().ToList();
            }
            return this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true).Where(x => x.Date >= search).To<WorkingTimeExceptionBookingModel>().ToList();
        }
        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId)
        {
            var wtex = this.userRepository.All().Where(u => u.Id == userId)
                 .Select(c => c.Company)
                 .Select(w => w.WorkingTime)
                 .Select(x => x.WorkingTimeException.Where(x=>x.IsRequested == false).Where(x=>x.Date < DateTime.UtcNow)).FirstOrDefault();
            foreach (var item in wtex)
            {
                this.weRepository.HardDelete(item);
               
            }
            this.weRepository.SaveChangesAsync();
        }
    }
}