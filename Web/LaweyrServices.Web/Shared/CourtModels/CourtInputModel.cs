using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.CourtModels
{
    public class CourtInputModel
    {
        public string? TownName { get; set; }

        [Required]
        public string? CourtName { get; set; }

        [Required]
        [Url]
        public string? CourtUrl { get; set; }

    }
}
