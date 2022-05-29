using LaweyrServices.Web.Shared.AreasCompanyModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyersLawFirmViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string AreaName { get; set; }

        public IEnumerable<AreasCompanyViewModel> AreasCompanies { get; set; }

        public string? WebSite { get; set; }

        public string Languages { get; set; }

        public string? ImgUrl { get; set; }

        public string TownName { get; set; }

        public bool FreeFirstAppointment { get; set; }

        public bool NoWinNoFee { get; set; }

        public bool FixedCost { get; set; }

        public bool MeetingClientLocation { get; set; }

        public string? PhoneNumbers { get; set; }

        public bool IsPublicPhoneNuber { get; set; }

        public string? LawFirmName { get; set; }

        public string? EarlyTime { get; set; }

        public string Address { get; set; }

        public int ReviewsCount { get; set; }

        public bool IsOwnerPermision { get; set; }

        public double AverageGrade { get; set; }

        public WorkingTimeViewModel WorkingTime { get; set; }
    }
}
