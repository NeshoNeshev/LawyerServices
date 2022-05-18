using LaweyrServices.Web.Shared.RatingModels;

namespace LawyerServices.Services.Data
{
    public interface IRatingService
    {
        public Task<string> CreateRatingAsync(RatingInputModel model);

        public Task<bool> CensoredRatingReviewAsync(string ratingId);

        public Task<List<RatingsViewModel>> GetAllRatingsAsync();

        public Task<bool> ExistingRatingAsync(string wteId);

    }
}
