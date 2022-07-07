using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.CourtModels
{
    public class CourtViewModel : IMapFrom<Court>
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? CourtUrl { get; set; }
    }
}
