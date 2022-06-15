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
        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequestsByDayOfWeekMeetingAsync(string lawyerId, DateTime date);
        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequstsByLawyerId(string lawyerId);
        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllNotaryRequsts(string userId);

        public  Task<WorkingTimeExceptionBookingModel> GetRequestById(string wteId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequsts(string userId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequestsByDayOfWeekAsync(string lawyerId, DateTime date);

        public Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetOverRequestsForUserIdAsync(string userId, DateTime date);

        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId);

        public  Task SetIsCanceledAsync(CancelCurrentWteInputModel model, string lawyerId);

        public Task<bool> SetNotSHowUpAsync(string wteId);

        public Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetRequestsForUserIdAsync(string userId);

        public Task SetWorkingTimeExceptionToFreeAsync(string wteId, string userId, string lawyerId);

        public Task CancelAppointmentFromDateAsync(CancelAppointmentForOneDateInputModel model, string userId);

        public Task CancelAppointmentInRangeAsync(CancelAppointmentInputModel model, string userId);

        public Task<bool> FreeRequestByWteIdAsync(string wteId);

        public Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>> GetMeetingWorkingTimeException(string userId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetEarliestWteAsync(string lawyerId, DateTime date);

        public Task DeleteNotaryAppointmentAsync(Appointment model);
        public Task<int> GetAppointmentsCountAsync ();
    }
}