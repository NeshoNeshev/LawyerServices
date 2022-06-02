using LaweyrServices.Web.Shared.ContactModels;
using LawyerServices.Common;
using LawyerServices.Services.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailSender emailSender;
        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost("PostContact")]
        public async Task<IActionResult> PostContact(ContactInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                
            }
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine(inputModel.Names);
                builder.AppendLine(inputModel.Content);
                await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, inputModel.Email, "neshev1978@gmail.com", "Съобщение от контактната форма", builder.ToString()) ;

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
           
            return Ok();
        }
    }
}
