using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IWorkingTimeExceptionService wteService;

        public DashboardController(IWorkingTimeExceptionService wteService)
        {
            this.wteService = wteService;
        }


        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllRequestsByDayOfWeek")]
        public List<WorkingTimeExceptionBookingModel> GetAllRequestsByDayOfWeek(DateTime searchDate)
        {


            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.wteService.GetAllRequestsByDayOfWeek(userId, searchDate).ToList();
            return response;
        }
    }
}
