using LawyerServices.Common.AreasOfActivityViewModels;
using LawyerServices.Common.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Services.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerServices.Common.LawyerViewModels
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

        public WorkingTimeViewModel WorkingTime { get; set; }
    }
}
