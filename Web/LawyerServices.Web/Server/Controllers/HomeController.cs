using LawyerServices.Services.Data;
using LawyerServices.Web.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LawyerServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController
    {
        private readonly ICountryService countryService;
        public HomeController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public CountryViewModel Get()
        {
            var country = this.countryService.GetAll<CountryViewModel>().FirstOrDefault();

            return country;
        }

    }
}
