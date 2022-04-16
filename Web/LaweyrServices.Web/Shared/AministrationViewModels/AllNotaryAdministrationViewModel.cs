using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class AllNotaryAdministrationViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string Address { get; set; }

        public string? OfficeName { get; set; }

        public string? WebSite { get; set; }

        public string? AboutText { get; set; }

        public string ImgUrl { get; set; }

        public bool PhoneVerification { get; set; }
    }
}
