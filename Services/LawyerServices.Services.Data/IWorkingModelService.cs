namespace LawyerServices.Services.Data
{
    public interface IWorkingModelService
    {
        public Task CreateInitialWorkingModel(string companyId);
    }
}
