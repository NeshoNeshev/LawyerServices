using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AreasOfActivityViewModels
{
    public class AreasOfActivityViewModel : IMapFrom<AreasOfActivity>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActiv { get; set; }

        public string? LawyerId { get; set; }
    }
}
