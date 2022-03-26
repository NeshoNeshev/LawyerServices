using LaweyrServices.Web.Shared.RatingModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;

namespace LawyerServices.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;
        public RatingService(IDeletableEntityRepository<Review> reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<byte> CreateRating(RatingInputModel model)
        {
            var review = new Review()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = model.CompanyId,
                UserId = model.UserId,
                Rating = model.Greade,
            };

            await this.reviewRepository.AddAsync(review);
            this.reviewRepository.SaveChangesAsync();

            return review.Rating;
        }

        public bool CensoredRatingReview(string ratingId)
        { 
            var rating = this.reviewRepository.All().FirstOrDefault(r => r.Id == ratingId);
            if (rating == null) return false;

            rating.IsCensored = true;

            this.reviewRepository.Update(rating);
            this.reviewRepository.SaveChangesAsync();
            return true;
        }
    }
}
