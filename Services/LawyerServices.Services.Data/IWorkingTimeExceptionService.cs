using LawyerServices.Common.WorkingTimeModels;

namespace LawyerServices.Services.Data
{
    public interface IWorkingTimeExceptionService
    {
        public Task SendRequestToLawyer(string lawyerId, string wteId, string userId);

        public int GetRequstsCount(string lawyerId);

        public  Task<WorkingTimeExceptionBookingModel> AcceptRequest(string wteId);
    }
}
