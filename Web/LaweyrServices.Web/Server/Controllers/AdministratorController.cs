using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRequestsService requestService;
        private readonly ITownService townService;
        private readonly ILawyerService lawyerService;
        private readonly INotaryService notaryService;
        private readonly ILawFirmService lawyfirmService;
        public AdministratorController(
            IImageService imageService, ITownService townService,
            IRequestsService requestService,
            ILawyerService lawyerService, IWorkingTimeExceptionService wteService, INotaryService notaryService, IUserService userService, ILawFirmService lawyfirmService)
        {
            this.townService = townService;
            this.requestService = requestService;
            this.userService = userService;
            this.lawyerService = lawyerService;
            this.notaryService = notaryService;
            this.userService = userService;
            this.lawyfirmService = lawyfirmService;
        }

        [HttpPost("CreateUser")]
        public void CreateUser([FromBody] CreateLawyerModel lawyerModel)
        {
           
            //chseck
            if (!ModelState.IsValid)
            {

            }

            var response = this.lawyerService.CreateLawyer(lawyerModel);
            if (!response.IsCompleted)
            {

            }
            var companyId = response.Result;
            this.userService.CreateUserAsync(lawyerModel, companyId);

            //todo
            //this.requestService.SetIsApproved();
        }

        [HttpPost("CreateNotary")]
        public IActionResult CreateNotary([FromBody] CreateNotaryModel notaryModel)
        {
            var existigPhone = this.userService.ExistingPhoneNumber(notaryModel.PhoneNumber);
            //chseck
            if (!ModelState.IsValid)
            {

            }
            if (existigPhone == true)
            {
                ModelState.AddModelError(nameof(notaryModel.PhoneNumber),
                       "Телефонът съществува"
                       );

                return BadRequest(ModelState);
            }
            var response = this.notaryService.CreateNotary(notaryModel);
            if (!response.IsCompleted)
            {

            }
            var companyId = response.Result;
            this.userService.CreateNotaryUserAsync(notaryModel, companyId);
            return Ok();
            //todo
            //this.requestService.SetIsApproved();
        }

        [HttpPost("CreateLawFirm")]
        public IActionResult CreateLawFirm([FromBody] LawFirmInputModel lawFirmModel)
        {
            //chseck
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(lawFirmModel),
                        "Invalid model"
                        );

            }

            var response = this.lawyfirmService.CreateLawFirm(lawFirmModel);

            if (!response.IsCompleted)
            {
                return BadRequest();
            }
            return Ok(response);
            //todo
            //this.requestService.SetIsApproved();
        }
        [HttpPost("CreateLawyerAndFirmName")]
        public void CreateLawyerAndFirmName([FromBody] CreateLawyerModel lawyerModel)
        {
            //chseck
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(lawyerModel),
                       "Invalid model"
                       );
            }
            var lawFirmId = this.lawyfirmService.GetIdByName(lawyerModel.OfficeName);
            var response = this.lawyerService.CreateLawyer(lawyerModel);
            if (!response.IsCompleted)
            {

            }
            var companyId = response.Result;
            this.userService.CreateUserAsync(lawyerModel, companyId);
            this.lawyerService.AddLawyerToLawFirm(companyId, lawFirmId);
        }
        [HttpGet("GetAllRequests")]
        public async Task<IEnumerable<RequestViewModel>> GetAllRequests()
        {
            var allRequests = this.requestService.GetAllRequests<RequestViewModel>();

            return allRequests;

        }

        [HttpGet("GetAllLawyers")]
        public IEnumerable<AllLawyersAdministrationViewModel> GetAllLawyers()
        {
            var lawyers = this.lawyerService.GetAllLawyers<AllLawyersAdministrationViewModel>();

            return lawyers;


        }

        [HttpGet("GetAllLawFirms")]
        public IEnumerable<LawFirmAdministrationViewModel> GetAllLawFirms()
        {
            var lawFirms = this.lawyfirmService.GetAll<LawFirmAdministrationViewModel>();

            return lawFirms;


        }

        [HttpGet("GetAllNotary")]
        public IEnumerable<AllNotaryAdministrationViewModel> GetAllNotary()
        {
            var notary = this.notaryService.GetAllNotary<AllNotaryAdministrationViewModel>();

            return notary;


        }

        [HttpPut("EditNotary")]
        public IActionResult EditNotary([FromBody]EditNotaryModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.notaryService.EditNotaryByAdministrator(model);
                if (result.IsCompletedSuccessfully)
                {
                    if (result.IsCompletedSuccessfully)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
           
        }


        [HttpGet("GetTowns")]
        public IEnumerable<TownViewModel> GetTowns()
        {
            //User.Identity.GetUserId();
            var towns = this.townService.GetAll<TownViewModel>();

            return towns;
        }
        [HttpGet("GetRequestsCount")]
        public int GetRequestsCount()
        {
            var count = this.requestService.GetRequestsCount();

            return count;
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<AllUsersAdministatorViewModel> GetAllUsers()
        {
            var result = this.userService.GetAll<AllUsersAdministatorViewModel>();

            return result;
        }
        [HttpPut("EditLawyer")]
        public IActionResult EditLawyer([FromBody]EditLawyerModel? inputModel)
        {
            var result = this.lawyerService.EditLawyerByAdministrator(inputModel);
            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("EditUser")]
        public IActionResult EditUser([FromBody] UserEditModel? inputModel)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.userService.EditUser(inputModel);
                if (!result.IsCompletedSuccessfully)
                {
                    return BadRequest();
                }
            }
           
            return Ok();
        }
    }
}
