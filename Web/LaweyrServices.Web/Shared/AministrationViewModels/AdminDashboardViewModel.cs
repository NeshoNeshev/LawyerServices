using LaweyrServices.Web.Shared.RatingModels;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class AdminDashboardViewModel
    {
        public int? UsersCount { get; set; }

        public int? LawyersCount { get; set; }

        public int? NotaryCount { get; set; }

        public int? AppointmentCount { get; set; }

        public ICollection<RatingsViewModel>? Ratings { get; set; }
    }
}
