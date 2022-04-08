using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.DateModels
{
    public class CancelAppointmentInputModel
    {
    
        [Required]
        public DateTime FirstDate { get; set; }

        [Required]
        public DateTime LastDate { get; set; }

        [Required]
        public string? ReasonFromCanceled { get; set; }
    }
}
