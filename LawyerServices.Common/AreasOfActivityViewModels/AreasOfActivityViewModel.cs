using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.AreasOfActivityViewModels
{
    public class AreasOfActivityViewModel : IMapFrom<AreasOfActivity>
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string BindingName { get; set; }
    }
}
