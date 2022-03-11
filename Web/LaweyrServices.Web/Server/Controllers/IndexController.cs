using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaweyrServices.Web.Server.Controllers
{
 
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ITownService townService;
        private readonly ILawyerService lawyerService;
        private readonly IAreasOfActivityService areaService;

        public IndexController(ITownService townService, ILawyerService lawyerService, IAreasOfActivityService areaService)
        {
            this.townService = townService;
            this.lawyerService = lawyerService;
            this.areaService = areaService;
        }

        [HttpGet]
        public IEnumerable<TownViewModel> Get()
        { 
             var towns = this.townService.GetAll<TownViewModel>();

            return towns;
        }

        [HttpGet("GetAreas")]
        public IEnumerable<AreasOfActivityViewModel> GetAreas()
        {
            var areas = this.areaService.GetAll<AreasOfActivityViewModel>();


            return areas;
        }
    }
}
