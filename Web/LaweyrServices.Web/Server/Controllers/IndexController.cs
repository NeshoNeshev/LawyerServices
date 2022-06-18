using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.IndexViewModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
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
        private readonly ILawyerService lawyerService;
        public IndexController(ITownService townService, IAreasOfActivityService areaService, ILawFirmService lawFirmService, INotaryService notaryService, ILawyerService lawyerService)
        {
            this.townService = townService;
            this.areaService = areaService;
            this.lawFirmService = lawFirmService;
            this.notaryService = notaryService;
            this.lawyerService = lawyerService;
        }

        [HttpGet]
        public async Task<IndexViewModel> Get()
        {

            var indexModel = new IndexViewModel();

            indexModel.Towns = await this.townService.GetAll<TownViewModel>();
            indexModel.Areas = await this.areaService.GetAll<AreasOfActivityViewModel>();
            indexModel.LawFirms = await this.lawFirmService.GetAll<LawFirmIndexViewModel>();
            indexModel.AllNotarys = await this.notaryService.GetAllNotary<NotaryIndexViewModel>();
            indexModel.LawyerNames = await this.lawyerService.GetAllLawyers<LawyerIndexModel>();
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
