using LaweyrServices.Web.Shared.CourtModels;

namespace LawyerServices.Services.Data
{
    public interface ICourtService
    {
        public Task CreateCourt(CourtInputModel model);
    }
}
