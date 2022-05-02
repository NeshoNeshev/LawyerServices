using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.NotaryModels
{
    public class NotaryViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string? Names { get; set; }

        public string? WebSite { get; set; }

        public string Address { get; set; }

        public string TownName { get; set; }

        public string? PhoneNumbers { get; set; }

        public string? ImgUrl { get; set; }
        public string? AboutText { get; set; }

        public bool WorkInSaturday { get; set; }

        public bool WorkInSunday { get; set; }
    }
}
