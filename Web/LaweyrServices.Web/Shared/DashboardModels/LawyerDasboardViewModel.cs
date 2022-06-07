using LaweyrServices.Web.Shared.WorkingTimeModels;

namespace LaweyrServices.Web.Shared.DashboardModels
{
    public class LawyerDasboardViewModel
    {
        public int ClientCount { get; set; }

        public int MeetingtCount { get; set; }

        public int UsersCount { get; set; }

        public string? LawFirmId { get; set; }

        public bool IsOwner { get; set; }

        public IEnumerable<WorkingTimeExceptionBookingModel>? wteModel { get; set; }

        public IEnumerable<WorkingTimeExceptionBookingModel>? meetingModel { get; set; }
    }
}
