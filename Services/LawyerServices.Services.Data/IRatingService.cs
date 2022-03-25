using LaweyrServices.Web.Shared.RatingModels;

namespace LawyerServices.Services.Data
{
    public interface IRatingService
    {
        public  Task<byte> CreateRating(RatingInputModel model);
    }
}
