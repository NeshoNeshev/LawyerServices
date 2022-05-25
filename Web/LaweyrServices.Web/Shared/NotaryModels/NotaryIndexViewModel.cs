using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.NotaryModels
{
    public class NotaryIndexViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string TownId { get; set; }
    }
}
