using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Web.Shared
{
    public class TownViewModel : IMapFrom<Town>
    {
        public string Id { get; init; }

        public string Name { get; init; }
    }
}
