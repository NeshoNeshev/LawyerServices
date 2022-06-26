using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.ModeratorModels
{
    public class ModeratorViewModel : IMapFrom<Moderator>
    {
        public string Id { get; set; }

        public string? Email { get; set; }

        public string? LawFirmName { get; set; }
    }
}
