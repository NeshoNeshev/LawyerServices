using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
