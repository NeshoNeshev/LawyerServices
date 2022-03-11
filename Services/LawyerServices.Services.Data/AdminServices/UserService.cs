﻿using LaweyrServices.Web.Shared.AdministratioInputModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace LawyerServices.Services.Data.AdminServices
{
    public class UserService : IUserService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository, IServiceProvider serviceProvider)
        {
            this.userRepository = userRepository;
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.userRepository.All().OrderBy(x => x.UserName);
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
        public void CreateUserAsync(CreateLawyerModel lawyerModel, string companyId)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var passsGenerator = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                UserName = lawyerModel.Email,
                Email = lawyerModel.Email,
                EmailConfirmed = true,
                CompanyId = companyId,
                PhoneNumber = lawyerModel.PhoneNumber,               
            };

            var  result = userManager.CreateAsync(user, "nesho1978").GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, lawyerModel.Role.ToString()).GetAwaiter().GetResult();
            }
           
        }
    }
}
