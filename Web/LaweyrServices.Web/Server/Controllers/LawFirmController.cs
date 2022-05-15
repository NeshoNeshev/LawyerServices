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
