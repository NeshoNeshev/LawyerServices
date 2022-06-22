using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Common;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using LawyerServices.Services.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Text;

namespace LawyerServices.Services.Data.AdminServices
{
    public class UserService : IUserService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IImageService imageService;
        private readonly IEmailSender emailSender;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository, IServiceProvider serviceProvider, IImageService imageService, IEmailSender emailSender)
        {
            this.userRepository = userRepository;
            this.serviceProvider = serviceProvider;
            this.imageService = imageService;
            this.emailSender = emailSender;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.userRepository.All().Where(x => x.FirstName != null).OrderBy(x => x.FirstName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<bool> UserNextWteNumberIsSmalForTwo(string userId, DateTime date)
        {
               
            var count = await this.userRepository.All().Where(x => x.Id == userId).SelectMany(x => x.WorkingTimeExceptions).Where(x => x.StarFrom >= date).CountAsync();
            if (count >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> GetUserFirstName(string userId)
        {

            var names = await this.userRepository.All().Where(x => x.Id == userId).Select(x => new { x.FirstName, x.LastName }).ToArrayAsync();
            var first = names[0].FirstName;
            var second = names[0].LastName;

            var result = first + " " + second;
            return result;

        }

        public string? GetUserId(ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async void CreateUserAsync(CreateLawyerModel lawyerModel, string companyId)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var imageUrl = this.imageService.AddFolderAndImage(lawyerModel.Names);
            var passsGenerator = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                UserName = lawyerModel.Email,
                Email = lawyerModel.Email,
                EmailConfirmed = true,
                CompanyId = companyId,
                PhoneNumber = lawyerModel.PhoneNumber,
                ImgUrl = imageUrl,
               
            };

            var result = userManager.CreateAsync(user, passsGenerator).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, lawyerModel.Role.ToString()).GetAwaiter().GetResult();
                await SendRegistration(lawyerModel.Names, lawyerModel.Email);
            }

        }
        public async Task CreateModreatorAsync(string email, string moderatorId)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var passsGenerator = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                ModeratorId= moderatorId,
              
            };

            var result = userManager.CreateAsync(user, passsGenerator).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
               userManager.AddToRoleAsync(user, "Moderator").GetAwaiter().GetResult();
                
               await SendRegistration(email, email);
            }

        }
        private async Task SendRegistration(string names, string userEmail)
        {
            var messageBody = new StringBuilder();
            messageBody.AppendLine($"Благодарим ви, че се присъединихте към нас {names}. Вашият профил беше създаден.");
            messageBody.AppendLine($"Можете да промените паролата от <a href=\"{GlobalConstants.ResetPasswordUrl}\"> тук</a>");

            await emailSender.SendEmailAsync(GlobalConstants.PlatformEmail, "Правен портал", userEmail,
                "Нова регистрация в Правен портал",
                messageBody.ToString()
                );

        }
        public async void CreateNotaryUserAsync(CreateNotaryModel notaryModel, string companyId)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var imageUrl = this.imageService.AddFolderAndImage(notaryModel.Names);
            var passsGenerator = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                UserName = notaryModel.Email,
                Email = notaryModel.Email,
                EmailConfirmed = true,
                CompanyId = companyId,
                PhoneNumber = notaryModel.PhoneNumber,
                ImgUrl = imageUrl,
            };

            var result = userManager.CreateAsync(user, passsGenerator).GetAwaiter().GetResult();

            if (result.Succeeded)
            {

                userManager.AddToRoleAsync(user, notaryModel.Role.ToString()).GetAwaiter().GetResult();
                await SendRegistration(notaryModel.Names, notaryModel.Email);
            }

        }
        public async Task<ApplicationUserViewModel> GetUserInformationAsync(string? userId)
        {
            var user = await this.userRepository.All().Where(u => u.Id == userId).To<ApplicationUserViewModel>().FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> ExistingPhoneNumber(string phoneNumber)
        {
            var exist = await this.userRepository.All().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (exist == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ExistingEmailAddress(string email)
        {
            var exist = await this.userRepository.All().FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());
            if (exist == null)
            {
                return false;
            }
            return true;
        }
        public async Task EditUserAsync(UserEditModel model)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == model.Id);
            if (user != null)
            {
                user.IsBan = (bool)model.IsBan;
                this.userRepository.Update(user);
                await this.userRepository.SaveChangesAsync();

            }
        }
        public async Task EditUserProfileByUserAsync(EditUserInformationInputModel model)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == model.Id);
            try
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.IsReminderForComing = model.IsReminderForComing;
                user.IsReserved = model.IsReserved;
                user.IsSendSms = model.IsSendSms;
                this.userRepository.Update(user);
                await this.userRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("User not created");
            }
        }
        public async Task<int> GetUsersCountAsync()
        {
            var count = await this.userRepository.All().Where(x => x.FirstName != null).CountAsync();

            return count;
        }
    }
}
