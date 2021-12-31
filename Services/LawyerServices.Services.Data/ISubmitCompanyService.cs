

using LawyerServices.Common;

namespace LawyerServices.Services.Data
{
    public interface ISubmitCompanyService
    {
        public Task CreateAsync(SubmitApplicationModel model);
    }
}
