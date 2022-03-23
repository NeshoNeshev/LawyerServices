using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeViewModel : IMapFrom<WorkingTime>
    {
        public IEnumerable<WorkingTimeExceptionViewModel> WorkingTimeExceptions { get; set; }
    }
}
