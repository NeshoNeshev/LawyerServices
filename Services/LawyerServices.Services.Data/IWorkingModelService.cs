namespace LawyerServices.Services.Data
{
    public interface IWorkingModelService
    {
        public Task CreateInitialWorkingModelAsync(string companyId);
    }
}
