using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Data.Models.SystemModels
{
    public abstract class BaseModel<TKey> : IDateInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
