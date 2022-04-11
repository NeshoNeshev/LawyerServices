using LaweyrServices.Web.Shared.LawFirmModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class LawFirmService : ILawFirmService
    {
        private readonly IDeletableEntityRepository<LawFirm> lawFirmrepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        public LawFirmService(IDeletableEntityRepository<LawFirm> lawFirmrepository)
        {
            this.lawFirmrepository = lawFirmrepository;
        }

        public async Task CreateLawFirm(LawFirmInputModel model)
        {

            var town = this.townRepository.All().FirstOrDefault(x => x.Name.ToLower() == model.Town.ToLower());
            if (town == null) return;
            var lawFirm = new LawFirm() 
            {
                Name = model.Name,
                Town = town,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                About = model.About,

            };

            await this.lawFirmrepository.AddAsync(lawFirm);
            this.lawFirmrepository.SaveChangesAsync();
        }
    }
}
