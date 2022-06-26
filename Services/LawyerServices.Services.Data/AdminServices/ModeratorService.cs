using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data.AdminServices
{
    public class ModeratorService : IModeratorService
    {
        private readonly IUserService userService;
        private readonly IDeletableEntityRepository<Moderator> moderatorRepository;
        private readonly ILawFirmService lawFirmService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        public ModeratorService(IUserService userService, IDeletableEntityRepository<Moderator> moderatorRepository, ILawFirmService lawFirmService, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userService = userService;
            this.moderatorRepository = moderatorRepository;
            this.lawFirmService = lawFirmService;
            this.userRepository = userRepository;
        }
        public async Task<string> GetModeratorId(string userId)
        {
            var moderatorId = await this.userRepository.All().Where(x => x.Id == userId).Select(x => x.ModeratorId).FirstOrDefaultAsync();
            return moderatorId;
        }
        public async Task<IEnumerable<T>> GetAllModerators<T>()
        {
            IQueryable<Moderator> query = this.moderatorRepository.All();


            return await query.To<T>().ToListAsync();
        }

        public async Task CreateModerator(ModeratorInputModel model)
        {
            var lawFirmId = this.lawFirmService.GetIdByName(model.LawFirm);
            if (lawFirmId != null)
            {
               
                try
                {
                    var moderator = new Moderator()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = model.Email,
                        LawFirmId = lawFirmId,

                    };
                    await this.moderatorRepository.AddAsync(moderator);
                    await this.moderatorRepository.SaveChangesAsync();
                    await this.userService.CreateModreatorAsync(moderator.Email,moderator.Id);
                }
                catch (Exception)
                {

                    throw new InvalidOperationException();
                }
            }      
        }
        public async Task StopAccountAsync(string id)
        {
           
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.ModeratorId == id);
            user.IsBan = true;

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }
        public async Task RestoreAccountAsync(string id)
        {

            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.ModeratorId == id);
            user.IsBan = false;

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }
    }
}
