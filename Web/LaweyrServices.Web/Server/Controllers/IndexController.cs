using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.IndexViewModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Mvc;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ITownService townService;
        private readonly IAreasOfActivityService areaService;
        private readonly ILawFirmService lawFirmService;
        private readonly INotaryService notaryService;
        public IndexController(ITownService townService, IAreasOfActivityService areaService, ILawFirmService lawFirmService, INotaryService notaryService)
        {
            this.townService = townService;
            this.areaService = areaService;
            this.lawFirmService = lawFirmService;
            this.notaryService = notaryService;
        }

        [HttpGet]
        public async Task<IndexViewModel> Get()
        {
            var indexModel = new IndexViewModel();

            indexModel.Towns = await this.townService.GetAll<TownViewModel>();
            indexModel.Areas = await this.areaService.GetAll<AreasOfActivityViewModel>();
            indexModel.LawFirms = await this.lawFirmService.GetAll<LawFirmIndexViewModel>();
            indexModel.AllNotarys = await this.notaryService.GetAllNotary<NotaryIndexViewModel>();
            return indexModel;
        }

        [HttpGet("GetAreas")]
        public async Task<IEnumerable<AreasOfActivityViewModel>> GetAreas()
        {
            var areas = await this.areaService.GetAll<AreasOfActivityViewModel>();


            return areas;
        }

       
    }
}
