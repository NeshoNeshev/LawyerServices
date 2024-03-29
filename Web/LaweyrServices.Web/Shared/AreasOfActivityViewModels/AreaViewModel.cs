﻿using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AreasOfActivityViewModels
{
    public class AreaViewModel : IMapFrom<AreasOfActivity>
    {
        public string? Id { get; set; }

        public string? Name { get; set; }
    }
}
