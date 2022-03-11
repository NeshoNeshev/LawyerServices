using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AreasOfActivityViewModels
{
    public class AreasCompanyViewModel : IMapFrom<AreasCompany>
    {
        public AreasOfActivity AreasOfActivity { get; set; }
    }
}
