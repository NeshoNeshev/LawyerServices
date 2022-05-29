using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.DateModels
{
    public class CancelAppointmentForOneDateInputModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Причината за отказа е задължителна !")]
        [StringLength(70, ErrorMessage ="Причината не може да е по дълга от 70 знака")]
        public string? ReasonFromCanceled { get; set; }

        public  string? LawyerId { get; set; }
    }
}
