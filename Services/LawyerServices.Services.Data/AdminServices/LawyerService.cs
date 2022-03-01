using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Shared.AdministrationInputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using LawyerServices.Services.Mapping;
using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Common.WorkingTimeModels;
using Microsoft.AspNetCore.Components.Forms;

namespace LawyerServices.Services.Data.AdminServices
{
    public class LawyerService : ILawyerService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        private readonly IDeletableEntityRepository<WorkingTime> workingRepository;

        private readonly IServiceProvider serviceProvider;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IImageService imageService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public LawyerService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Town> townRepository,
            IServiceProvider serviceProvider,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<WorkingTime> workingRepository, IImageService imageService)
        {
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
            this.serviceProvider = serviceProvider;
            this.userRepository = userRepository;
            this.workingRepository = workingRepository;
            this.imageService = imageService;
        }

        public async Task CreateLawyer(CreateLawyerModel lawyerModel)
        {

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(lawyerModel.Email);

            var passsGenerator = Guid.NewGuid().ToString();
            var imageUrl = this.imageService.AddFolderAndImage(lawyerModel.Names);
            var town = this.townRepository.All().FirstOrDefault(t => t.Name == lawyerModel.TownName);

            if (town == null) return;
            if (user != null) return;

            var workingTime = new WorkingTime()
            {
                Id = Guid.NewGuid().ToString(),
                Name = lawyerModel.Email,
                IsActiv = true,
            };
            await this.workingRepository.AddAsync(workingTime);
            this.workingRepository.SaveChangesAsync();

            var company = new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Names = lawyerModel.Names,
                OfficeName = lawyerModel.OfficeName,
                TownId = town.Id,
                Profession = lawyerModel.Role,
                Address = lawyerModel.AddressLocation,
                WorkingTimeId = workingTime.Id,
                ImgUrl = imageUrl,
            };

            await this.companyRepository.AddAsync(company);

            this.companyRepository.SaveChangesAsync();

           
            
            //Todo: password
            var result = await userManager.CreateAsync(
                     new ApplicationUser
                     {
                         UserName = lawyerModel.Email,
                         Email = lawyerModel.Email,
                         EmailConfirmed = true,
                         CompanyId = company.Id,
                         PhoneNumber = lawyerModel.PhoneNumber,
                         
                     }, "nesho1978");;

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
        public async Task EditImage(InputFileChangeEventArgs args ,string userId)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            if (company != null)
            {
               var newImg =  await this.imageService.UserImageUploadAsync(args);

                company.ImgUrl = newImg;

                this.companyRepository.Update(company);
                this.companyRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Image not exist");
            }
        }
        public async Task<string> ExistingLawyerByPhone(string phoneNumber)
        {

            var exist = this.userRepository.All().FirstOrDefault(p => p.PhoneNumber == phoneNumber);

            if (exist == null)
            {
                return string.Empty;
            }
            var phone = exist.PhoneNumber;
            return phone;
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

        public IEnumerable<T> GetAllLawyers<T>(int? count = null)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer"));
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        //Todo : implement categorySearch
        public IEnumerable<T> SearchAllLawyersByTownAndCategory<T>(string townId)
        {
            IQueryable<Company> query = this.companyRepository.All().Where(x => x.Profession == (Profession)Enum.Parse(typeof(Profession), "Lawyer")).Where(l => l.TownId == townId);

            return query.To<T>().ToList();
        }
        public LawyerListItem GetLawyerById(string userId)
        {
            var workingTime = this.userRepository.All().Where(u => u.Id == userId).Select(x=>x.WorkingTimeExceptions).FirstOrDefault();

            var lawyer = this.companyRepository.All().Where(u => u.Id == userId).To<LawyerListItem>().FirstOrDefault();

            //var lawyerToReturn = new LawyerViewModel();
            //lawyerToReturn.LawyerListItem = lawyer;
            //lawyerToReturn.WorkingTime = new List<WorkingTimeExceptionViewModel>();

            //if (workingTime != null)
            //{
            //    var aa = workingTime.Where(x => x.StarFrom.Date >= DateTime.UtcNow.Date);
            //    foreach (var item in aa)
            //    {
            //        lawyerToReturn.WorkingTime.Add(new WorkingTimeExceptionViewModel() { StarFrom = item.StarFrom, EndTo = item.EndTo, Date = item.Date, AppointmentType = item.AppointmentType });
            //    }
            //}
           
            
    
            
            return lawyer;
        }

    }
}
