using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;

namespace LawyerServices.Services.Data
{
    public interface IWorkingTimeExceptionService
    {
        public Task SendRequestToLawyer(UserRequestModel? userRequestModel);

        public int GetRequstsCount(string lawyerId);

        public  Task<WorkingTimeExceptionBookingModel> GetRequestById(string wteId);

        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequsts(string userId);

        public IEnumerable<WorkingTimeExceptionBookingModel> GetAllRequestsByDayOfWeek(string userId, DateTime search);

        public void DeleteWorkingTimeExceptionWhenDateIsOver(string userId);

        public  Task SetIsApproved(string wteId);

        public Task<bool> SetNotSHowUp(string wteId);

        public IEnumerable<WorkingTimeExceptionUserViewModel> GetRequestsForUserId(string userId);

        public void SetWorkingTimeExceptionToFree(string wteId, string userId);

        public Task CancelAppointmentFromDate(CancelAppointmentForOneDateInputModel model, string userId);

        public Task CancelAppointmentInRange(CancelAppointmentInputModel model, string userId);
    }
}