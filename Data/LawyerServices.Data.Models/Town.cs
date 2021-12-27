using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Town : BaseDeletableModel<string>
    {
        public Town()
        {
            this.Lawyers = new HashSet<Lawyer>();
        }
        public string Name { get; set; }

        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Lawyer> Lawyers { get; set; }
    }
}
