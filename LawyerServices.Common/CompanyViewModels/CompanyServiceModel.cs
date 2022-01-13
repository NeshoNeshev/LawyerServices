using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.CompanyViewModels
{
    public class CompanyServiceModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
    }
}
