using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class AllLawyersAdministrationViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string? WebSite { get; set; }

        public string Address { get; set; }

        public string? OfficeName { get; set; }

        public string? Languages { get; set; }

        public string? HeaderText { get; set; }

        public string? AboutText { get; set; }

        public string? Jurisdiction { get; set; }

        public string? LicenceDate { get; set; }

        public string? LastChecked { get; set; }

        public bool PhoneVerification { get; set; }

        public string? ImgUrl { get; set; }
    }
}
