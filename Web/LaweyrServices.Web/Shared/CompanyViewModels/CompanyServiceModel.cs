using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.CompanyViewModels
{
    public class CompanyServiceModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string Address { get; set; }

        public string ImgUrl { get; set; }
    }
}