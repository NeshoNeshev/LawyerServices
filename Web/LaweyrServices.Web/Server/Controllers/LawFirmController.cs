using LaweyrServices.Web.Shared.LawFirmModels;
using LawyerServices.Services.Data;
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
        public async Task<LawFirmViewModel> Get(string? lawFirmId)
        {
            var lawFirm = await this.lawFirmService.GetLawFirm(lawFirmId);
            var areas = lawFirm.Companies.SelectMany(x => x.AreasCompanies);
            if (areas != null)
                lawFirm.Areas = new List<string>();
            foreach (var item in areas)
            {
                
                if (!lawFirm.Areas.Contains(item.AreasOfActivity.Name))
                {
                    lawFirm.Areas.Add(item.AreasOfActivity.Name);
                }
            }

            foreach (var item in lawFirm.Companies)
            {
                if (item.WorkingTime.WorkingTimeExceptions.Any(x => x.StarFrom >= DateTime.Now && x.IsRequested == false && x.IsCanceled == false && x.AppointmentType == "Час за консултация"))
                {
                    string? earlyTime = item.WorkingTime.WorkingTimeExceptions.OrderBy(x => x.StarFrom).Where(x => x.StarFrom >= DateTime.Now && x.IsRequested == false && x.IsCanceled == false && x.AppointmentType == "Час за консултация").Select(x => x.StarFrom).First().ToString();
                    item.EarlyTime = earlyTime;
                }

            }
            return lawFirm;
        }
    }
}
