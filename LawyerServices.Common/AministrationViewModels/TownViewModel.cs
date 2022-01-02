using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.AministrationViewModels
{
    public class TownViewModel : IMapFrom<Town>
    {
        public string Name { get; set; }
    }
}
