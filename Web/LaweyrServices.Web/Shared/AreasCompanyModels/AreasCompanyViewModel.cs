using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AreasCompanyModels
{
    public class AreasCompanyViewModel : IMapFrom<AreasCompany>
    {
        public AreasOfActivity AreasOfActivity { get; set; }
    }
}
