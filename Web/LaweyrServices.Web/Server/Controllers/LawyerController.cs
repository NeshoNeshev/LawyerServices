using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LawyerController : ControllerBase
    {
        private readonly ITownService townService;
        private readonly IAreasOfActivityService areaService;
        private readonly ISearchService searchService;
        private readonly ILawyerService lawyerService;
        private readonly IWorkingTimeExceptionService wteService;
        private readonly ICompanyService companyService;
        private readonly IImageService imageService;
        private readonly IFixedPriceService fixedPriceService;

        public LawyerController(ITownService townService, IAreasOfActivityService areaService, ISearchService searchService, ILawyerService lawyerService, IWorkingTimeExceptionService wteService, ICompanyService companyService, IImageService imageService, IFixedPriceService fixedPriceService)
        {
            this.townService = townService;
            this.areaService = areaService;
            this.searchService = searchService;
            this.lawyerService = lawyerService;
            this.wteService = wteService;
            this.companyService = companyService;
            this.imageService = imageService;
            this.fixedPriceService = fixedPriceService;
        }

        [HttpGet("WteCount")]
        public int WteCount()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var count = this.wteService.GetRequstsCount(userId);

            return count;
        }

        [HttpGet("Search")]
        public IEnumerable<LawyerListItem> Search( string? name, string? town, string? area)
        {
            var lawyers =  this.searchService.Search(name, town, area).Result;

            return lawyers;
        }
        [HttpGet("Count")]
        public async Task<int> Count( string? name, string? town, string? area)
        {
            var count =  this.searchService.Search(name, town, area).Result.Count();

            return count;
        }
        [HttpGet("GetTowns")]
        public IEnumerable<TownViewModel> GetTowns()
        {
            var towns = this.townService.GetAll<TownViewModel>();

            return towns;
        }

        [HttpGet("GetAreas")]
        public IEnumerable<AreasOfActivityViewModel> GetAreas()
        {
            var areas = this.areaService.GetAll<AreasOfActivityViewModel>();
            return areas;
        }

        [HttpGet("FindLawyerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindLawyerById(string lawyerId)
        {
            var response = this.lawyerService.ExistingLawyerById(lawyerId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("GetLawyerById")]
        public LawyerListItem GetLawyerById(string lawyerId)
        {
            
            var lawyer = this.lawyerService.GetLawyerById(lawyerId);
            return lawyer;
        }

        [HttpGet("GetAllRequestsByDayOfWeek")]
        public List<WorkingTimeExceptionBookingModel> GetAllRequestsByDayOfWeek(DateTime searchDate)
        {
        
            
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.wteService.GetAllRequestsByDayOfWeek(userId, searchDate).ToList();
            return response;
        }

        [HttpGet("DeleteWorkingTimeExceptionWhenDateIsOver")]
        public IActionResult DeleteWorkingTimeExceptionWhenDateIsOver()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                this.wteService.DeleteWorkingTimeExceptionWhenDateIsOver(userId);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("GetAllRequsts")]
        public List<WorkingTimeExceptionBookingModel> GetAllRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response  = this.wteService.GetAllRequsts(userId).ToList();

            return response;
        }

        [HttpGet("GetMoreInformation")]
        public  MoreInformationInputModel? GetMoreInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response =  this.companyService.GetMoreInformation(userId);

            return response;
        }

        [HttpGet("GetAllAreasByCompanyId")]
        public List<string> GetAllAreasByCompanyId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var areas = areaService.GetAllAreasByCompanyId(userId).ToList();

            return areas;
        }

        [HttpPost("OnSaveInformation")]
        public async Task<IActionResult> OnSaveInformation([FromBody]MoreInformationInputModel? moreInformation)
        {
            if (moreInformation == null || !this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(moreInformation),
                        "More information can not be null " 
                        );
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.companyService.CreateMoreInformation(moreInformation, userId);
            return Ok(response);
        }
        [HttpPost("CreateAreas")]
        public async Task<IActionResult> CreateAreas([FromBody] IList<string>? areasToAdd)
        {
            if (areasToAdd == null || !this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(areasToAdd),
                        "Areas can not be null "
                        );
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.areaService.CreateAreas(areasToAdd, userId);
            return Ok(response);
        }

        [HttpGet("GetAllAreas")]
        public List<AreasOfActivityViewModel> GetAllAreas()
        {
            var areas = this.areaService.AllAreas().ToList();

            return areas;
        }
        [HttpGet("GetAllAppointments")]
        public IList<Appointment> GetAllAppointments()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.companyService.GetAllAppointments(userId);

            return response;
        }

        [HttpPost("SaveCompanyAppointments")]
        public IActionResult SaveCompanyAppointments([FromBody]Appointment data)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.companyService.SaveCompanyAppointments(data, userId);
            if (response != null) return Ok();

            return BadRequest();
        }
        [HttpPost("EditImage")]
        public void EditImage(string name,byte[] bytes)
        {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //get aweyter get result
            //var imagePath =  this.imageService.UserImageUploadAsync(args);
            //this.lawyerService.UpdateLawyerImage(userId, imagePath);
            string extension;
            extension = name.Substring(name.Length - 4);

            this.lawyerService.EditImage(bytes, userId, extension);

        }

        [HttpGet("GetLawyerWorkingTimeExteption")]
        public AppointmentViewModel GetLawyerWorkingTimeExteption(string appointmentId)
        {
            var appointment = this.lawyerService.GetLawyerWorkingTimeExteption(appointmentId);

            return appointment;
        }

        [HttpPost("PostBooking")]
        public IActionResult PostBooking(UserRequestModel? userRequestModel)
        {
            userRequestModel.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.wteService.SendRequestToLawyer(userRequestModel);
            if (!response.IsCompleted)
            {
                return BadRequest(response);
            }
            return Ok(response);
           
        }
        [HttpPost("PostApproved")]
        public  IActionResult PostApproved([FromBody]string Id)
        {
            if (this.ModelState.IsValid)
            {
                var response = this.wteService.SetIsApproved(Id);
                return Ok(response);
            }
          
            return BadRequest();
            
        }

        [HttpPost("PostFixedCost")]
        public IActionResult PostFixedCost([FromBody] FixedCostInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = this.companyService.GetCompanyId(userId);
            model.lawyerId = lawyerId;
            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model),
                        "Areas can not be null "
                        );
            }
            var response = this.fixedPriceService.CreateService(model);

            return Ok(response);
            

        }
    }
}