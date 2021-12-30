using LawyerServices.Web.Shared.SubmitApplicationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerServices.Web.Server.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        public AdministrationController()
        {

        }

        [HttpPost]
        [Route("api/Administrator/CreateUser")]
        public void CreateUser([FromBody]SubmitApplicationModel model)
        { 
        
        }
    }
}
