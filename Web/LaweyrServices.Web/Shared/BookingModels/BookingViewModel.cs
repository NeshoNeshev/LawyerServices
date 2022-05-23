using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.UserModels;

namespace LaweyrServices.Web.Shared.BookingModels
{
    public class BookingViewModel
    {
        public LawyerBookingViewModel? LawyerBookingViewModel { get; set; }

        public ApplicationUserViewModel? ApplicationUserViewModel { get; set; }

        public AppointmentViewModel? AppointmentViewModel { get; set; }
    }
}
