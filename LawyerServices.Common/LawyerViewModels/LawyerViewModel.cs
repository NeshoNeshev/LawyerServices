using LawyerServices.Common.WorkingTimeModels;

namespace LawyerServices.Common.LawyerViewModels
{
    public class LawyerViewModel
    {
        public LawyerListItem LawyerListItem { get; set; }

        public List<WorkingTimeExceptionViewModel> WorkingTime { get; set; }
    }
}
