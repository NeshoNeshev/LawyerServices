using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class AllLawyersModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string WebSite { get; set; }


        public string? ImgUrl { get; set; }

        public string Address { get; set; }

        public string TownName { get; set; }

        public bool FreeFirstAppointment { get; set; }

        public bool NoWinNoFee { get; set; }

        public bool FixedCost { get; set; }

        public bool MeetingClientLocation { get; set; }

        public string? LawFirmName { get; set; }

        public string? LawFirmId { get; set; }

        public string? EarlyTime { get; set; }

        public int ReviewsCount { get; set; }

        public WorkingTimeViewModel WorkingTime { get; set; }
    }
}
