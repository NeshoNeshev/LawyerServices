using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.RatingModels
{
    public class RatingsViewModel : IMapFrom<Review>
    {
        public string Id { get; set; }

        public bool IsCensored { get; set; }

        public bool IsModerated { get; set; }
        public byte TrustworthyRating { get; set; }

        public byte ServiceRating { get; set; }

        public string? Commentary { get; set; }

        public string? UserFirstName { get; set; }

        public string? UserLastName { get; set; }

        public DateTime DateReview { get; set; }
    }
}
