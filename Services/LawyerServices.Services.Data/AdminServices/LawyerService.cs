using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using LawyerServices.Services.Mapping;

using Microsoft.AspNetCore.Components.Forms;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.DateModels;

namespace LawyerServices.Services.Data.AdminServices
{
    public class LawyerService : ILawyerService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<WorkingTime> workingRepository;
        private readonly IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IImageService imageService;
        private readonly IRequestsService requestsService;


        public LawyerService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Town> townRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<WorkingTime> workingRepository,
            IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository, IImageService imageService, IRequestsService requestsService)
        {
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
            this.userRepository = userRepository;
            this.workingRepository = workingRepository;
            this.workingTimeExceptionRepository = workingTimeExceptionRepository;
            this.imageService = imageService;
            this.requestsService = requestsService;
        }

        public async Task<string> CreateLawyer(CreateLawyerModel lawyerModel)
        {

           // var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ////var user = await userManager.FindByNameAsync(lawyerModel.Email);

            //var passsGenerator = Guid.NewGuid().ToString();
            ////var imageUrl = this.imageService.AddFolderAndImage(lawyerModel.Names);
            var town = this.townRepository.All().FirstOrDefault(t => t.Name == lawyerModel.TownName);

            //if (town == null) return;
            //if (user != null) return;

            var workingTime = new WorkingTime()
            {
                Id = Guid.NewGuid().ToString(),
                Name = lawyerModel.Email,
                IsActiv = true,
            };
            await this.workingRepository.AddAsync(workingTime);
            this.workingRepository.SaveChangesAsync();

            var imgUrl = this.imageService.AddFolderAndImage(lawyerModel.Names);

            var company = new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Names = lawyerModel.Names,
                OfficeName = lawyerModel.OfficeName,
                TownId = town.Id,
                Profession = lawyerModel.Role,
                Address = lawyerModel.AddressLocation,
                WorkingTimeId = workingTime.Id,
                ImgUrl = imgUrl,
                RequestId = lawyerModel.RequestId,
                PhoneVerification = lawyerModel.PhoneVerification,
                
            };

            await this.companyRepository.AddAsync(company);

            this.companyRepository.SaveChangesAsync();
            await this.requestsService.SetIsApproved(lawyerModel.RequestId);
            return company.Id;
            
            //Todo: password
          
        }
        public async Task EditImage(byte[] bytes ,string userId, string extension)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            if (company != null)
            {
                if (company.ImgUrl != null)
                {
                    DeleteImage(company.ImgUrl);
                }
               
                var imageName = Guid.NewGuid().ToString();
                //get aweyter get result
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", $"{imageName}{extension}");
                
                using (var ms = new MemoryStream(bytes))
                {
                    
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        
                        ms.WriteTo(fs);
                    }
                }
                var imgUrl = $"/images/{imageName}{extension}";
                company.ImgUrl = imgUrl;

                this.companyRepository.Update(company);
                this.companyRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Image not exist");
            }
        }
        private void DeleteImage(string umgUrl)
        {
          
            var imageName = umgUrl.Split("/", StringSplitOptions.RemoveEmptyEntries).ToArray();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", imageName[1]);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
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
            lawyer.WorkingTime.WorkingTimeExceptions = lawyer.WorkingTime.WorkingTimeExceptions.Where(x => x.StarFrom >= DateTime.UtcNow).Where(x=>x.IsRequested == false);
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
        public bool ExistingLawyerById(string lawyerId)
        {
            var exist = this.companyRepository.All().Any(x=>x.Id == lawyerId);
            if (!exist)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateLawyerImage(string userId, string imgPath)
        {
            var company = this.userRepository.All().Where(u => u.Id == userId).Select(x => x.Company).FirstOrDefault();
            if (company != null)
            {
               
                company.ImgUrl = imgPath;

                this.companyRepository.Update(company);
                this.companyRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Image not exist");
            }
           
        }

        public AppointmentViewModel GetLawyerWorkingTimeExteption(string appointmentId)
        {
            var appointment = this.workingTimeExceptionRepository.All().Where(x => x.Id == appointmentId).To<AppointmentViewModel>().FirstOrDefault();

            return appointment;
        }
    }
}
