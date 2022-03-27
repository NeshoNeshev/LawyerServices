using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.FixedCostModels
{
    public class FixedCostUpdateModel
    {
        [Required]
        public string? FixedCostId { get; set; }

        [Required]
        [StringLength(20)]
        public string? Name { get; set; }

        [Required]
        public double? Price { get; set; }
    }
}
