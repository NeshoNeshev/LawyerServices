using LaweyrServices.Web.Shared.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawFirmModels
{
    public class LawFirmViewModel : IMapFrom<LawFirm>
    {
        public string Id { get; set; }
        public string? Name { get; set; }

        public string TownName { get; set; }

        public string? Address { get; set; }

        public string? About { get; set; }

        public string? PhoneNumbers { get; set; }

        public string? ImgUrl { get; set; }

        public string? WebSiteUrl { get; set; }

        public string Email { get; set; }

        public string? FacebookUrl { get; set; }

        public string? LinkedinUrl { get; set; }

        public IEnumerable<string> Areas { get; set; }

        public IEnumerable<LawyersLawFirmViewModel> Companies { get; set; }
    }
}
