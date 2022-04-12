using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawFirmModels
{
    public class LawFirmIndexViewModel : IMapFrom<LawFirm>
    {
        public string Id { get; set; }

        public string? Name { get; set; }
    }
}
