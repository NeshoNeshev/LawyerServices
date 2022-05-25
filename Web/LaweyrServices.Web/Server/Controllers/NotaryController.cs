using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
 
    public class NotaryController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly INotaryService notaryService;
        private readonly ITownService townService;
        private readonly ICompanyService companyService;
        public NotaryController(ISearchService searchService, INotaryService notaryService, ITownService townService, ICompanyService companyService)
        {
            this.searchService = searchService;
            this.notaryService = notaryService;
            this.townService = townService;
            this.companyService = companyService;
        }

        [HttpGet("OnGet")]
        public async Task<NotaryPageViewModel> OnGet(string? townName)
        {
            var response = new NotaryPageViewModel();
            response.AllNotarys = await this.notaryService.GetAllNotaryByTown<NotaryViewModel>(townName);
            response.AllTowns = await this.townService.GetAll<TownViewModel>();

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
        [Authorize(Roles = "Notary")]
        [HttpGet("GetNotaryByNotaryId")]
        public async Task<NotaryViewModel> GetNotaryByNotaryId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var companyId = await this.companyService.GetCompanyIdAsync(userId);
            var response = await this.notaryService.GetNotaryById(companyId);

            return response;
        }
        [Authorize(Roles = "Notary")]
        [HttpPut("EditNotary")]
        public async Task<IActionResult> EditNotary([FromBody] EditNotaryModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.notaryService.EditNotaryByAdministratorAsync(model);
                return Ok();
            }
            return BadRequest();

        }
    }
}
