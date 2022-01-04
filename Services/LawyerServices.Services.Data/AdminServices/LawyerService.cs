﻿using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Shared.AdministrationInputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerServices.Services.Data.AdminServices
{
    public class LawyerService : ILawyerService
    {
        private IDeletableEntityRepository<Company> companyRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        public LawyerService(IDeletableEntityRepository<Company> companyRepository, IDeletableEntityRepository<Town> townRepository, IServiceProvider serviceProvider, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
            this.serviceProvider = serviceProvider;
            this.userRepository = userRepository;
        }

        public async Task CreateLawyer(CreateLawyerModel lawyerModel)
        {
            
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(lawyerModel.Email);

            var passsGenerator = Guid.NewGuid().ToString();

            var town = this.townRepository.All().FirstOrDefault(t => t.Id == lawyerModel.TownId);

            if (town == null) return;
            if (user != null) return;

            var company = new Company()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = lawyerModel.FirstName,
                LastName = lawyerModel.LastName,
                WebSite = lawyerModel.WebSite,
                OfficeName = lawyerModel.OfficeName,
                TownId = lawyerModel.TownId,
                Profession = lawyerModel.Role,
                Address = lawyerModel.AddressLocation,
            };
            await this.companyRepository.AddAsync(company);

            await this.companyRepository.SaveChangesAsync();

            //Todo: password
            var result = await userManager.CreateAsync(
                     new ApplicationUser
                     {
                         UserName = lawyerModel.Email,
                         Email = lawyerModel.Email,
                         EmailConfirmed = true,
                         CompanyId = company.Id,
                         PhoneNumber = lawyerModel.PhoneNumber,
                     }, "nesho1978");
           
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
            var newUser = await userManager.FindByNameAsync(lawyerModel.Email);
            if (newUser != null)
            {
                await userManager.AddToRoleAsync(newUser, lawyerModel.Role.ToString());
            }
        }
        public bool ExistingLawyerByPhone(string phoneNumber)
        {

            var exist =  this.userRepository.All().Any(p=>p.PhoneNumber == phoneNumber);
            if (exist)
            {
                return true;
            }
            return false;
        }

        public bool ExistingLawyerByEmail(string email)
        {
            var exist = this.userRepository.All().Any(p => p.Email == email);
            if (exist)
            {
                return true;
            }
            return false;
        }
    }
}
