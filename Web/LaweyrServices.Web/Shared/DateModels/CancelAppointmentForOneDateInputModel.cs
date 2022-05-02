using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.DateModels
{
    public class CancelAppointmentForOneDateInputModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Причината за отказа е задължителна !")]
        public string? ReasonFromCanceled { get; set; }
    }
}
