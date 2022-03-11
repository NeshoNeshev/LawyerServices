using LaweyrServices.Web.Shared.WorkingTimeModels;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyerViewModel
    {
        public LawyerListItem LawyerListItem { get; set; }

        public List<WorkingTimeExceptionViewModel>? WorkingTime { get; set; }
    }
}
