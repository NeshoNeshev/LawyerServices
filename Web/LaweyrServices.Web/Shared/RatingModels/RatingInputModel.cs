using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.RatingModels
{
    public class RatingInputModel
    {

        public string? UserId { get; set; }


        public string? CompanyId { get; set; }

        [Required]
        [Range(1, 5)]
        public byte TrustworthyGreade { get; set; }

        [Required]
        [Range(1, 5)]
        public byte ServiceGreade { get; set; }

       
        public string? Commentary { get; set; }

        public string WteId { get; set; }
    }
}
