using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Lawyer : BaseDeletableModel<string>
    {
        public Lawyer()
        {

        }

        public string Office { get; set; }

        public string WebSite { get; set; }

        public string YearsOfExperience { get; set; }

        public string TownId { get; set; }

        public Town Town { get; set; }

        public ICollection<AreasOfActivity> AreasOfActivities { get; set; }
    }
}
