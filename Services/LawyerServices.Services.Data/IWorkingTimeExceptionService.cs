using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;

namespace LawyerServices.Services.Data
{
    public interface IWorkingTimeExceptionService
    {
        public Task SendRequestToLawyerAsync(UserRequestModel? userRequestModel);

        public Task<int> GetUserRequstsCountAsync(string lawyerId);

        public Task<int> GetMeetingRequstsCountAsync(string lawyerId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllNotaryRequsts(string userId);

        public  Task<WorkingTimeExceptionBookingModel> GetRequestById(string wteId);

        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequsts(string userId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequestsByDayOfWeekAsync(string lawyerId);

        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId);

        public  Task SetIsCanceledAsync(CancelCurrentWteInputModel model, string lawyerId);

        public Task<bool> SetNotSHowUpAsync(string wteId);

        public Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetRequestsForUserIdAsync(string userId);

        public Task SetWorkingTimeExceptionToFreeAsync(string wteId, string userId);

        public Task CancelAppointmentFromDateAsync(CancelAppointmentForOneDateInputModel model, string userId);

        public Task CancelAppointmentInRangeAsync(CancelAppointmentInputModel model, string userId);

        public Task<bool> FreeRequestByWteIdAsync(string wteId);

        public Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>> GetMeetingWorkingTimeException(string userId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetEarliestWteAsync(string lawyerId);

        public Task DeleteNotaryAppointmentAsync(Appointment model);
        public Task<int> GetAppointmentsCountAsync ();
    }
}