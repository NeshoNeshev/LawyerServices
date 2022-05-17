using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.RatingModels
{
    public class RatingsViewModel : IMapFrom<Review>
    {
        public string Id { get; set; }

        public byte Rating { get; set; }

        public string? Commentary { get; set; }

        public bool IsCensored { get; set; } = false;

        public string? UserId { get; set; }

        public string? CompanyId { get; set; }
    }
}
