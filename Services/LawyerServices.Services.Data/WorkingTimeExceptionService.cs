using LawyerServices.Services.Mapping;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.DateModels;
using LawyerServices.Common;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data
{
    public class WorkingTimeExceptionService : IWorkingTimeExceptionService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> weRepository;
        private readonly IDeletableEntityRepository<WorkingTime> workingRepo;
        public WorkingTimeExceptionService(IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<WorkingTimeException> weRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<WorkingTime> workingRepo)
        {
            this.companyRepository = companyRepository;
            this.weRepository = weRepository;
            this.userRepository = userRepository;
            this.workingRepo = workingRepo;
        }

        public async Task SendRequestToLawyerAsync(UserRequestModel? userRequestModel)
        {
            var workingTimeException = this.weRepository.All().FirstOrDefault(ex => ex.Id == userRequestModel.WorkingTimeExceptionId);
            if (workingTimeException == null) return;
            try
            {
                workingTimeException.IsRequested = true;
                workingTimeException.IsApproved = true;
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
            await this.weRepository.SaveChangesAsync();

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
            var wtexc = await this.weRepository.All().Where(w => w.Id == wteId).Where(x => x.IsRequested == true).To<WorkingTimeExceptionBookingModel>().FirstOrDefaultAsync();

            return wtexc;
        }
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllNotaryRequsts(string userId)
        {   

            var workingTimeId = await this.userRepository.All().Where(x => x.Id == userId).Select(x => x.Company).Select(x => x.WorkingTimeId).FirstOrDefaultAsync();
            var exc = await this.weRepository.All().Where(x => x.WorkingTimeId == workingTimeId).OrderBy(x=>x.StarFrom).To<WorkingTimeExceptionBookingModel>().ToListAsync();

            return exc;
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

        public async Task SetIsApprovedAsync(string wteId)
        {
            var wte = this.weRepository.All().Where(x => x.Id == wteId).FirstOrDefault();
            if (wte != null)
            {
                wte.IsApproved = false;
                wte.IsCanceled = true;
                this.weRepository.Update(wte);
                await this.weRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> SetNotSHowUpAsync(string wteId)
        {
            var wte = this.weRepository.All().FirstOrDefault(w => w.Id == wteId);

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
                await this.weRepository.SaveChangesAsync();

                this.userRepository.Update(user);
                await this.userRepository.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetRequestsForUserIdAsync(string userId)
        {
            var wteExceptions = await this.weRepository.All().Where(w => w.UserId == userId).To<WorkingTimeExceptionUserViewModel>().OrderByDescending(x => x.StarFrom).ToListAsync();

            return wteExceptions;
        }
        public async Task SetWorkingTimeExceptionToFreeAsync(string wteId, string userId)
        {
            var wte = this.weRepository.All().FirstOrDefault(w => w.Id == wteId);

            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

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
            await this.weRepository.SaveChangesAsync();

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task CancelAppointmentFromDateAsync(CancelAppointmentForOneDateInputModel model, string lawyerId)
        {
            //todo check aftermorning
            var lawyer = this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions.Where(x => x.Date.Date == model.Date.Date)).FirstOrDefault();
            if (lawyer == null) return;
            //var wte = lawyer.WorkingTime.WorkingTimeExceptions.Where(x=>x.Date.Date == model.Date.Date);

            if (lawyer.Any())
            {

                foreach (var item in lawyer)
                {
                    if (item.AppointmentType.ToLower() == GlobalConstants.Meeting.ToLower())
                    {
                        continue;
                    }
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
                await this.weRepository.SaveChangesAsync();
            }


        }

        public async Task CancelAppointmentInRangeAsync(CancelAppointmentInputModel model, string lawyerId)
        {
            var lawyer = this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime).Select(x => x.WorkingTimeExceptions.Where(x => x.Date >= model.FirstDate && x.Date <= model.LastDate)).FirstOrDefault();
            if (lawyer == null) return;
            //var lawyer = this.userRepository.All().FirstOrDefault(u => u.Id == userId).Company;
            //var wte = lawyer.WorkingTime.WorkingTimeExceptions.Where(x => x.Date.Date >= model.FirstDate && x.Date <= model.LastDate);

            if (lawyer.Any())
            {

                foreach (var item in lawyer)
                {
                    if (item.AppointmentType.ToLower() == GlobalConstants.Meeting.ToLower())
                    {
                        continue;
                    }
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
                await this.weRepository.SaveChangesAsync();
            }

        }

        public async Task<bool> FreeRequestByWteIdAsync(string wteId)
        {

            var wte = await this.weRepository.All().FirstOrDefaultAsync(x => x.Id == wteId);
            if (wte == null)
            {
                return false;
            }
            if (wte.IsRequested == true) return false;

            return true;
        }

        public async Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>> GetMeetingWorkingTimeException(string lawyerId)
        {
            var exceptions = new List<WorkingTimeExceptionMeetingViewModel>();
         
            var results = await  this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTime.WorkingTimeExceptions).FirstOrDefaultAsync();
            var  result = results.Where(x => x.AppointmentType == GlobalConstants.Meeting).ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var newExc = new WorkingTimeExceptionMeetingViewModel();
                    newExc.Id = item.Id;
                    newExc.MoreInformation = item.MoreInformation;
                    newExc.CaseNumber = item.CaseNumber;
                    newExc.Court = item.Court;
                    newExc.StarFrom = item.StarFrom;
                    newExc.TypeOfCase = item.TypeOfCase;
                    newExc.SideCase = item.SideCase;
                    exceptions.Add(newExc);
                }
            }

            return exceptions;
        }

        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetEarliestWteAsync(string lawyerId)
        {

            var wteId = await this.companyRepository.All().Where(x => x.Id == lawyerId).Select(x => x.WorkingTimeId).FirstAsync();
            var exception = await this.weRepository.All()
                .Where(x => x.WorkingTimeId == wteId)
                .Where(x => x.IsRequested == false && x.AppointmentType == GlobalConstants.Client && x.StarFrom > DateTime.Now).To<WorkingTimeExceptionBookingModel>().ToListAsync();
            return exception;
        }

        public async Task<int> GetAppointmentsCountAsync()
        {
            var count = await this.weRepository.All().Where(x => x.AppointmentType == GlobalConstants.Client && x.IsApproved == true).CountAsync();

            return count;
        }
    }
}