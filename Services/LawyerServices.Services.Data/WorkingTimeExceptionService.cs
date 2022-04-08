using LawyerServices.Services.Mapping;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LaweyrServices.Web.Shared.UserModels;
using System.Globalization;
using LaweyrServices.Web.Shared.DateModels;

namespace LawyerServices.Services.Data
{
    public class WorkingTimeExceptionService : IWorkingTimeExceptionService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;

        public WorkingTimeExceptionService(IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<WorkingTimeException> weRepository, 
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyRepository;
            this.weRepository = weRepository;
            this.userRepository = userRepository;
        }

        public async Task SendRequestToLawyer(UserRequestModel? userRequestModel)
        {
            var workingTimeException = this.weRepository.All().FirstOrDefault(ex => ex.Id == userRequestModel.WorkingTimeExceptionId);
            if (workingTimeException == null) return;
            try
            {
                workingTimeException.IsRequested = true;
                workingTimeException.UserId = userRequestModel.UserId;
                workingTimeException.FirstName = userRequestModel.FirstName;
                workingTimeException.LastName = userRequestModel.LastName;
                workingTimeException.PhoneNumber = userRequestModel.PhoneNumber;
                workingTimeException.Email = userRequestModel.Email;
                workingTimeException.MoreInformation = userRequestModel.MoreInformation;
            }
            catch (Exception)
            {

                throw new InvalidOperationException("SendRequestToLawyer Error");
            }
            this.weRepository.Update(workingTimeException);
            this.weRepository.SaveChangesAsync();

        }
        public int GetRequstsCount(string userId)
        {
            var lawyerId = this.userRepository.All().Where(x => x.Id == userId).Select(c => c.CompanyId).FirstOrDefault();
            if (lawyerId == null) return 0;
            var workingTimeExceptions = this.companyRepository.All().Where(l => l.Id == lawyerId).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions.Where(x => x.IsRequested == true)).FirstOrDefault();
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
            var exc = this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true && x.IsCanceled == false).To<WorkingTimeExceptionBookingModel>().ToList();
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
                return this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true).Where(x => x.Date.Date == search).To<WorkingTimeExceptionBookingModel>().ToList();
            }
            return this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).Where(x => x.IsRequested == true).Where(x => x.Date.Date > search).To<WorkingTimeExceptionBookingModel>().ToList();

        }
        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId)
        {
            var wtex = this.userRepository.All().Where(u => u.Id == userId)
                 .Select(c => c.Company)
                 .Select(w => w.WorkingTime)
                 .Select(x => x.WorkingTimeExceptions.Where(x => x.IsRequested == false).Where(x => x.Date < DateTime.UtcNow)).FirstOrDefault();
            foreach (var item in wtex)
            {
                this.weRepository.HardDelete(item);

            }
            this.weRepository.SaveChangesAsync();
        }
        //public void Request(string wteId, string)
        //{ 

        //}
        public async Task SetIsApproved(string wteId)
        {
            var wte = this.weRepository.All().Where(x => x.Id == wteId).FirstOrDefault();
            if (wte != null)
            {
                wte.IsApproved = true;
                this.weRepository.Update(wte);
                this.weRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> SetNotSHowUp(string wteId)
        {
            var wte = this.weRepository.All().FirstOrDefault(w=>w.Id == wteId);
            
            if (wte != null && wte.IsApproved == true)
            {
                var user = this.userRepository.All().FirstOrDefault(u => u.Id == wte.UserId);
                if (user != null)
                {
                    user.NotShowUpCount++;
                    wte.NotShowUp = true;
                }
                else
                {
                    return false;
                }
               
                this.weRepository.Update(wte);
                this.weRepository.SaveChangesAsync();

                this.userRepository.Update(user);
                this.userRepository.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<WorkingTimeExceptionUserViewModel> GetRequestsForUserId(string userId)
        { 
          var wteExceptions = this.weRepository.All().Where(w=>w.UserId == userId).To<WorkingTimeExceptionUserViewModel>().OrderByDescending(x=>x.StarFrom).ToList();

            return wteExceptions;
        }
        public void SetWorkingTimeExceptionToFree(string wteId, string userId)
        {
            var wte = this.weRepository.All().FirstOrDefault(w=>w.Id == wteId);

            var user = this.userRepository.All().FirstOrDefault(u=>u.Id == userId);

            wte.IsRequested = false;
            wte.IsApproved = false;
            wte.MoreInformation = null;
            wte.FirstName = null;
            wte.LastName = null;
            wte.PhoneNumber = null;
            wte.Email = null;
            wte.UserId = null;

            user.CancelledCount++;

            this.weRepository.Update(wte);
            this.weRepository.SaveChangesAsync();

            this.userRepository.Update(user);
            this.userRepository.SaveChangesAsync();
        }

        public async Task CancelAppointmentFromDate(CancelAppointmentForOneDateInputModel model, string userId)
        {
            //todo check aftermorning
            var lawyer = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x=>x.WorkingTime).Select(x=>x.WorkingTimeExceptions.Where(x=>x.Date.Date == model.Date.Date)).FirstOrDefault();
            if (lawyer == null) return;
            //var wte = lawyer.WorkingTime.WorkingTimeExceptions.Where(x=>x.Date.Date == model.Date.Date);

            if (lawyer.Any())
            {
                
                foreach (var item in lawyer)
                {
                    if (item.IsApproved == true || item.IsRequested == true)
                    {
                        item.IsCanceled = true;
                        item.ReasonFromCanceled = model.ReasonFromCanceled;
                        this.weRepository.Update(item);
                    }
                    else
                    {
                        this.weRepository.HardDelete(item);
                    }
                    

                }
                this.weRepository.SaveChangesAsync();
            }


        }

        public async Task CancelAppointmentInRange(CancelAppointmentInputModel model, string userId)
        {
            var lawyer = this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions.Where(x => x.Date >= model.FirstDate && x.Date <= model.LastDate)).FirstOrDefault();
            if (lawyer == null) return;
            //var lawyer = this.userRepository.All().FirstOrDefault(u => u.Id == userId).Company;
            //var wte = lawyer.WorkingTime.WorkingTimeExceptions.Where(x => x.Date.Date >= model.FirstDate && x.Date <= model.LastDate);

            if (lawyer.Any())
            {

                foreach (var item in lawyer)
                {
                    if (item.IsApproved == true || item.IsRequested == true)
                    {
                        item.IsCanceled = true;
                        item.ReasonFromCanceled = model.ReasonFromCanceled;
                        this.weRepository.Update(item);
                    }
                    else
                    {
                        this.weRepository.HardDelete(item);
                    }


                }
                this.weRepository.SaveChangesAsync();
            }

        }
    }
}