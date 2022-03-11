

using LaweyrServices.Web.Shared;

namespace LawyerServices.Services.Data
{
    public interface ISubmitCompanyService
    {
        public Task<bool> CreateRequestAsync(SubmitApplicationModel model);
    }
}
