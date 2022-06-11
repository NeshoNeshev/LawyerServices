using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyerBookingViewModel : IMapFrom<Company>
    {
        public string? Id { get; set; }

        public string? Names { get; set; }

        public string? ImgUrl { get; set; }

        public string? Address { get; set; }

        public double AverageGrade { get; set; }

        public string? TownName { get; set; }

        public int ReviewsCount { get; set; }
    }
}
