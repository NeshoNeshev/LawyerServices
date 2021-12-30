using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Web.Shared
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public string Id { get; init; }

        public string Name { get; init; }


        public ICollection<TownViewModel> Towns { get; init; }
    }
}
