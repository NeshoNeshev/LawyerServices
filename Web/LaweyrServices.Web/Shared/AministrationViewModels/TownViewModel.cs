using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class TownViewModel : IMapFrom<Town>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
