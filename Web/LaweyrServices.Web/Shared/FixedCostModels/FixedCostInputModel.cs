﻿using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.FixedCostModels
{
    public class FixedCostInputModel
    {
        public string? lawyerId { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(30, ErrorMessage = "Името не може да е по дълго от 30 занка")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Цената е задължителна")]
        public double? Price { get; set; }
    }
}
