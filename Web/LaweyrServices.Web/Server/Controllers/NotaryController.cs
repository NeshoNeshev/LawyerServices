using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotaryController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly INotaryService notaryService;
        private readonly ITownService townService;
        public NotaryController(ISearchService searchService, INotaryService notaryService, ITownService townService)
        {
            this.searchService = searchService;
            this.notaryService = notaryService;
            this.townService = townService;
        }

        [HttpGet("OnGet")]
        public NotaryPageViewModel OnGet()
        {
            var response = new NotaryPageViewModel();
            response.AllNotarys = this.notaryService.GetAllNotary<NotaryViewModel>();
            response.AllTowns = this.townService.GetAll<TownViewModel>();

            return response;
        }

        [HttpGet("OnSearch")]
        public async Task<IEnumerable<NotaryViewModel>> OnSearch(string? townName)
        {
            var response = await this.searchService.SearchNotaryAsync(townName);

            return response;
        }
        [HttpGet("GetNotary")]
        public async Task<NotaryViewModel> GetNotary(string? id)
        {
           
            var response = await this.notaryService.GetNotaryById(id);
            
            return response;
        }
    }
}
