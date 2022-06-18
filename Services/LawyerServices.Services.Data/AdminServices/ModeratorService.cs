using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
                    this.userService.CreateModreatorAsync(moderator.Email,moderator.Id);
                }
                catch (Exception)
                {

                    throw new InvalidOperationException();
                }
            }
            
            
        }
    }
}
