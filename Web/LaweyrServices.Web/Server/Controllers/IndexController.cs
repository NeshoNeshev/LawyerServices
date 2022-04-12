using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.IndexViewModels;
using LaweyrServices.Web.Shared.LawFirmModels;
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
        private readonly ILawFirmService lawFirmService;

        public IndexController(ITownService townService, ILawyerService lawyerService, IAreasOfActivityService areaService, ILawFirmService lawFirmService)
        {
            this.townService = townService;
            this.lawyerService = lawyerService;
            this.areaService = areaService;
            this.lawFirmService = lawFirmService;
        }

        [HttpGet]
        public IndexViewModel Get()
        {
            var indexModel = new IndexViewModel();
            // var towns = this.townService.GetAll<TownViewModel>();
            //var areas = this.areaService.GetAll<AreasOfActivityViewModel>();
            indexModel.Towns = this.townService.GetAll<TownViewModel>();
            indexModel.Areas = this.areaService.GetAll<AreasOfActivityViewModel>();
            indexModel.LawFirms = this.lawFirmService.GetAll<LawFirmIndexViewModel>();
            return indexModel;
        }

        [HttpGet("GetAreas")]
        public IEnumerable<AreasOfActivityViewModel> GetAreas()
        {
            var areas = this.areaService.GetAll<AreasOfActivityViewModel>();


            return areas;
        }

       
    }
}
