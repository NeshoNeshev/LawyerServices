using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class WorkingTime : BaseDeletableModel<string>
    {
        public WorkingTime()
        {
            this.WorkingTimeException = new HashSet<WorkingTimeException>();
            this.WorkingTimeDays = new HashSet<WorkingTimeDays>();
            this.Companies = new HashSet<Company>();
        }
        
        public string Name { get; set; }

        public bool IsActiv { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<WorkingTimeException> WorkingTimeException { get; set; }

        public virtual ICollection<WorkingTimeDays> WorkingTimeDays { get; set; }
    }
}
