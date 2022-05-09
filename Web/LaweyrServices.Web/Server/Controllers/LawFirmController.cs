using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class LawFirmController : ControllerBase
    {
        private readonly ILawFirmService lawFirmService;

        public LawFirmController(ILawFirmService lawFirmService)
        {
            this.lawFirmService = lawFirmService;
        }

        [HttpGet]
        public LawFirmViewModel Get(string? lawFirmId)
        {
            var lawFirm = this.lawFirmService.GetLawFirm(lawFirmId);
            var areas = lawFirm.Companies.Select(x => x.AreasCompanies.Select(c => c.AreasOfActivity.Name)).Distinct().FirstOrDefault();
            if (areas != null)
            lawFirm.Areas = areas;
            
            return lawFirm;
        }
    }
}
