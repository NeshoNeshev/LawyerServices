using LaweyrServices.Web.Shared;
using LawyerServices.Common;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Messaging;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace LawyerServices.Services.Data
{
    public class SubmitCompanyService : ISubmitCompanyService
    {
        private readonly IDeletableEntityRepository<Request> requestRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailSender emailSender;
        public SubmitCompanyService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IDeletableEntityRepository<Request> requestRepository, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.requestRepository = requestRepository;
            this.emailSender = emailSender;
        }
        public async Task<bool> CreateRequestAsync(SubmitApplicationModel model)
        {

            var existingUser = this.userManager.Users.Any(ph => ph.PhoneNumber == model.PhoneNumber);
            if (existingUser)
            {

                return false;
            }

            var request = new Request()
            {
                Id = Guid.NewGuid().ToString(),
                Address = model.Address,
                Town = model.Town,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Names = model.Names,
                //AcceptedTermOfUse = model.AcceptedTermOfUse,
            };

            await this.requestRepository.AddAsync(request);
            await this.requestRepository.SaveChangesAsync();
            await RequestApointmentЕmail(model.Names, model.Email, model.PhoneNumber);
            return true;
        }
        private async Task RequestApointmentЕmail(string lawyerNames, string email, string phone)
        {
            var messageBody = new StringBuilder();
            messageBody.AppendLine($"Имате изпратена заявка за присъединяване от {lawyerNames}. Телефон: {phone}, Имейл {email}");
           

            await emailSender.SendEmailAsync("neshevgmail@abv.bg", "Правен портал", GlobalConstants.PlatformEmail,
                "Заявка за присъединяване",
                messageBody.ToString()
                );
           
        }
    }
}
