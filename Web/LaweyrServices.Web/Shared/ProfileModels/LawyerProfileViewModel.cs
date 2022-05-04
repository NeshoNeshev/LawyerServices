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

        public string? Email { get; set; }

        public bool IsPublicPhoneNuber { get; set; }

        public bool IsOwner { get; set; }

        public string? HeaderText { get; set; }

        public string? AboutText { get; set; }
    }
}
