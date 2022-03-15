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
    }
}
