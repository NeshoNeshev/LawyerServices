using LaweyrServices.Web.Shared.CompanyViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeUserViewModel : IMapFrom<WorkingTime>
    {
        public string Id { get; set; }

        public virtual ICollection<CompanyServiceModel> Companies { get; set; }
    }
}
