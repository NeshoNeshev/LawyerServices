using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Town : BaseDeletableModel<string>
    {
        public Town()
        {
            this.Companies = new HashSet<Company>();
            this.LawFirms = new HashSet<LawFirm>();
           
        }
        public string Name { get; set; }

        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<LawFirm> LawFirms { get; set; }

    }
}
