using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class WorkingTime : BaseDeletableModel<string>
    {
        public WorkingTime()
        {
            this.WorkingTimeException = new HashSet<WorkingTimeException>();
        }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }

        public bool IsActiv { get; set; }

        public virtual ICollection<WorkingTimeException> WorkingTimeException { get; set; }

        public virtual ICollection<WorkingTimeDays> WorkingTimeDays { get; set; }
    }
}
