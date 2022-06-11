using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Mvc;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TownsAreaController : ControllerBase
    {
        private readonly ITownService townService;
        private readonly IAreasOfActivityService areasOfActivityService;
        public TownsAreaController(ITownService townService, IAreasOfActivityService areasOfActivityService)
        {
            this.townService = townService;
            this.areasOfActivityService = areasOfActivityService;
        }

        [HttpGet("GetTowns")]
        public async Task<ICollection<TownViewModel>> GetTowns()
        {
            var response = await this.townService.GetAll<TownViewModel>();

            return response.ToList();
        }
        [HttpGet("GetAreas")]
        public async Task<ICollection<AreaViewModel>> GetAreas()
        {
            var response = await this.areasOfActivityService.GetAll<AreaViewModel>();

            return response.ToList();
        }
    }
}
