﻿using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class EditNotaryModel
    {

        public string Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="Името не може да е по дълго от 50 символа")]
        public string Names { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "Адресът не може да е по дълго от 70 символа")]
        public string AddressLocation { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Името не може да е по дълго от 60 символа")]
        public string? OfficeName { get; set; }

        [Required]
        [Url]
        public string? WebSite { get; set; }

        [Required]
        [StringLength(1500, ErrorMessage = "Описанието не може да е по дълго от 1500 символа")]
        public string About { get; set; }

        public string? OfficeNumbers { get; set; }

        public string? OfficeEmails { get; set; }

        public bool WorkInSaturday { get; set; }

        public bool WorkInSunday { get; set; }
    }
}
