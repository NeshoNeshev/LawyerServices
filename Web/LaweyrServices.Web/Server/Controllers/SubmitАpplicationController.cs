using LaweyrServices.Web.Shared;
using LaweyrServices.Web.Shared.AministrationViewModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitАpplicationController : ControllerBase
    {
        private readonly ISubmitCompanyService submitService;

        private readonly ITownService townService;
        public SubmitАpplicationController(ITownService townService, ISubmitCompanyService submitService)
        {
            this.townService = townService;
            this.submitService = submitService;
        }

        [HttpGet]
        public IEnumerable<TownViewModel> Get()
        {
            var towns = this.townService.GetAll<TownViewModel>();

            return towns;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SubmitApplicationModel? model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                //this.ModelState.AddModelError(
                //   nameof(ProductInputModel.Name),
                //   $"Съществува продукт с това име {model.Name}");
                //model.CategoryDropDown = this.categoryDropDown.ToList();
                return BadRequest();
            }
            var result = this.submitService.CreateRequestAsync(model);
            if (result.Result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
