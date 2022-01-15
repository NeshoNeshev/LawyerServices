using LawyerServices.Common.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ICompanyService
    {
        public Task CreateMoreInformation(string languages, string education, string qualifications, string experience, string companyId);

        public Task<MoreInformationViewModel> GetMoreInformation(string companyId);
    }
}
