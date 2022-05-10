using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class AreasOfActivity : BaseDeletableModel<string>
    {
        public AreasOfActivity()
        {
            this.AreasCompanies = new HashSet<AreasCompany>();
        }

        public string Name { get; set; }

        public bool IsActiv { get; set; } = false;

        public virtual ICollection<AreasCompany> AreasCompanies { get; set; }
  
    }
}