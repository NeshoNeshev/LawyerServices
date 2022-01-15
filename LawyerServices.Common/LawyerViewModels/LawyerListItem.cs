using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.LawyerViewModels
{
    public class LawyerListItem : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AreaName { get; set; }
    }
}
