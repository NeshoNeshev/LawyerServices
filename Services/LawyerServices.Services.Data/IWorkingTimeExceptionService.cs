namespace LawyerServices.Services.Data
{
    public interface IWorkingTimeExceptionService
    {
        public Task SendRequestToLawyer(string lawyerId, string wteId, string userId);
    }
}
