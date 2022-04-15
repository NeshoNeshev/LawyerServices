using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class AllUsersAdministatorViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? ImgUrl { get; set; }

        public bool IsBan { get; set; }

        public byte CancelledCount { get; set; } = 0;

        public byte NotShowUpCount { get; set; } = 0;
    }
}
