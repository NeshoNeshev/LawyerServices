﻿using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyerIndexModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }
    }
}
