using LaweyrServices.Web.Shared.CourtModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Services.Data
{
    public class CourtService : ICourtService
    {
        private readonly IDeletableEntityRepository<Court> courtRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        public CourtService(IDeletableEntityRepository<Court> courtRepository, IDeletableEntityRepository<Town> townRepository)
        {
            this.courtRepository = courtRepository;
            this.townRepository = townRepository;
        }

        public async Task CreateCourt(CourtInputModel model)
        {
            var townId = await this.townRepository.All().Where(x => x.Name == model.TownName).Select(x => x.Id).FirstOrDefaultAsync();
            if (townId == null)
            {
                return;
            }
            var court = new Court()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.CourtName,
                CourtUrl = model.CourtUrl,
                TownId = townId,
            };

            await this.courtRepository.AddAsync(court);

            await this.courtRepository.SaveChangesAsync();

        }
    }
}
