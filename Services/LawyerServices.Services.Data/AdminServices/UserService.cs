using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace LawyerServices.Services.Data.AdminServices
{
    public class UserService : IUserService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IImageService imageService;
        private readonly ILawyerService lawyerService;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository, IServiceProvider serviceProvider, IImageService imageService, ILawyerService lawyerService)
        {
            this.userRepository = userRepository;
            this.serviceProvider = serviceProvider;
            this.imageService = imageService;
            this.lawyerService = lawyerService;
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
            
            var result = await userManager.CreateAsync(user, "nesho1978")/*.GetAwaiter().GetResult()*/;

            if (result.Succeeded)
            {
                if (lawyerModel.IsOwner)
                {
                    var roles = new List<string>() { "Moderator", lawyerModel.Role.ToString() };

                    userManager.AddToRolesAsync(user, roles).GetAwaiter().GetResult();
                }
                userManager.AddToRoleAsync(user, lawyerModel.Role.ToString()).GetAwaiter().GetResult();
            }

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

            var result = userManager.CreateAsync(user, "nesho1978").GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                
                userManager.AddToRoleAsync(user, notaryModel.Role.ToString()).GetAwaiter().GetResult();
            }

        }
        public async Task<ApplicationUserViewModel> GetUserInformationAsync(string userId)
        {
            var user = await this.userRepository.All().Where(u => u.Id == userId).To<ApplicationUserViewModel>().FirstOrDefaultAsync();

            return user;
        }

        public bool ExistingPhoneNumber(string phoneNumber)
        {
            var exist = this.userRepository.All().FirstOrDefault(x => x.PhoneNumber == phoneNumber);
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
