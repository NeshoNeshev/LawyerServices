using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyerListItem : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string AreaName { get; set; }

        public IEnumerable<AreasCompanyViewModel> AreasCompanies { get; set; }

        public string WebSite { get; set; }

        public string Address { get; set; }

        public string OfficeName { get; set; }

        public string Languages { get; set; }

        public string Education { get; set; }

        public string Qualifications { get; set; }

        public string Experience { get; set; }

        public string? ImgUrl { get; set; }

        public string TownName { get; set; }

        public bool FreeFirstAppointment { get; set; } 

        public bool NoWinNoFee { get; set; } 

        public bool FixedCost { get; set; }

        public DateTime? EarlyTime { get; set; }

        public WorkingTimeViewModel WorkingTime { get; set; }
    }
}
