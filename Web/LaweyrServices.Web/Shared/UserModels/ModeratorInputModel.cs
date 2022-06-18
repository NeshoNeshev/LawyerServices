using LawyerServices.Data.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.UserModels
{
    public class ModeratorInputModel
    {

        public string? LawFirm { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Role { get; set; }

        public bool IsOwner { get; set; } = false;
    }
}
