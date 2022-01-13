using AutoMapper;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.LawyerViewModels
{
    public class LawyersAreaOfActivityViewModel
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string Company { get; set; }

        public string Area { get; set; }
    }
}
