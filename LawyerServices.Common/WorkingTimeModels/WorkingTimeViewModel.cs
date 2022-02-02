using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.WorkingTimeModels
{
    public class WorkingTimeViewModel : IMapFrom<WorkingTime>
    {
        public IEnumerable<WorkingTimeExceptionViewModel> WorkingTimeException { get; set; }
    }
}
