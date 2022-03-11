using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public string Name { get; set; }
    }
}
