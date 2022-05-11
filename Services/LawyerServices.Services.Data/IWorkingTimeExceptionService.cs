using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;

namespace LawyerServices.Services.Data
{
    public interface IWorkingTimeExceptionService
    {
        public Task SendRequestToLawyerAsync(UserRequestModel? userRequestModel);

        public int GetRequstsCount(string lawyerId);

        public Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllNotaryRequsts(string userId);
        public  Task<WorkingTimeExceptionBookingModel> GetRequestById(string wteId);

        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequsts(string userId);

        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequestsByDayOfWeek(string userId, DateTime search);

        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId);

        public  Task SetIsApprovedAsync(string wteId);

        public Task<bool> SetNotSHowUpAsync(string wteId);

        public IEnumerable<WorkingTimeExceptionUserViewModel> GetRequestsForUserId(string userId);

        public Task SetWorkingTimeExceptionToFreeAsync(string wteId, string userId);

        public Task CancelAppointmentFromDateAsync(CancelAppointmentForOneDateInputModel model, string userId);

        public Task CancelAppointmentInRangeAsync(CancelAppointmentInputModel model, string userId);

        public bool FreeRequestByWteId(string wteId);

        public Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>> GetMeetingWorkingTimeException(string userId);
    }
}