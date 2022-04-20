using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.ProfileModels
{
    public class LawyerProfileViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string? Names { get; set; }

        public string? WebSite { get; set; }

        public string? Address { get; set; }

        public string? OfficeName { get; set; }

        public string? ImgUrl { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsOwner { get; set; }
    }
}
