using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Shared.AreasCompanyModels
{
    public class AreasCompanyViewModel : IMapFrom<AreasCompany>
    {
        public AreasOfActivity AreasOfActivity { get; set; }
    }

}
