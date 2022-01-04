using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.AministrationViewModels
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public string Name { get; set; }
    }
}
