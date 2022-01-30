using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.AreasOfActivityViewModels
{
    public class AreasCompanyViewModel : IMapFrom<AreasCompany>
    {
        public AreasOfActivity AreasOfActivity { get; set; }
    }
}
