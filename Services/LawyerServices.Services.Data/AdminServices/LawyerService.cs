using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;
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
        private readonly IDeletableEntityRepository<LawFirm> firmRepository;
        private readonly IImageService imageService;
        private readonly IRequestsService requestsService;


        public LawyerService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Town> townRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<WorkingTime> workingRepository,
            IDeletableEntityRepository<WorkingTimeException> workingTimeExceptionRepository,
            IImageService imageService, IRequestsService requestsService, IDeletableEntityRepository<LawFirm> firmRepository)
        {
            this.companyRepository = companyRepository;
            this.townRepository = townRepository;
            this.userRepository = userRepository;
            this.workingRepository = workingRepository;
            this.workingTimeExceptionRepository = workingTimeExceptionRepository;
            this.imageService = imageService;
            this.requestsService = requestsService;
            this.firmRepository = firmRepository;
        }

        public async Task<string> CreateLawyer(CreateLawyerModel lawyerModel)
        {
            var town = this.townRepository.All().FirstOrDefault(t => t.Name == lawyerModel.TownName);

            var workingTime = new WorkingTime()
            {
                Id = Guid.NewGuid().ToString(),
                Name = lawyerModel.Email,
                IsActiv = true,
            };
           
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

            await this.workingRepository.AddAsync(workingTime);
            this.workingRepository.SaveChangesAsync();



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

                    using var fs = new FileStream(filePath, FileMode.Create);

                    ms.WriteTo(fs);
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
            lawyer.WorkingTime.WorkingTimeExceptions = lawyer.WorkingTime.WorkingTimeExceptions.Where(x => x.StarFrom >= DateTime.UtcNow).Where(x=>x.IsRequested == false).Where(x=>x.IsCanceled == false);
          
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
        public async Task<string> AddLawyerToLawFirm(string lawFirmId, string lawyerId)
        {
            var lawfirm = this.firmRepository.All().FirstOrDefault(x => x.Id == lawFirmId);
            var lawyer = this.companyRepository.All().FirstOrDefault(x => x.Id == lawyerId);
            try
            {              
                if (lawfirm != null && lawyer != null)
                {
                    lawyer.LawFirmId = lawfirm.Id;
                    this.companyRepository.Update(lawyer);
                    this.companyRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Lawyer not created");
            }

            return lawfirm.Id;

        }
    }
}
