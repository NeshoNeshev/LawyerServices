using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.RatingModels
{
    public class RatingInputModel
    {
        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? CompanyId { get; set; }

        [Required]
        [Range(0,5)]
        public byte Greade { get; set; }

        [Required]
        [StringLength(60)]
        public string Commentary { get; set; }
    }
}
