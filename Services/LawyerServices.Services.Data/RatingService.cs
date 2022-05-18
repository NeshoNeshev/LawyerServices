using LaweyrServices.Web.Shared.RatingModels;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;
        public RatingService(IDeletableEntityRepository<Review> reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<string> CreateRatingAsync(RatingInputModel model)
        {
            try
            {
                var review = new Review()
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyId = model.CompanyId,
                    UserId = model.UserId,
                    TrustworthyRating = model.TrustworthyGreade,
                    ServiceRating = model.ServiceGreade,
                    Commentary = model.Commentary,
                    WteId = model.WteId,
                    DateReview = DateTime.Now,
                };

                await this.reviewRepository.AddAsync(review);
                await this.reviewRepository.SaveChangesAsync();
                return review.Id;
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Rating not created");
            }
           
        }

        public async Task<bool> CensoredRatingReviewAsync(string ratingId)
        { 
            var rating = await this.reviewRepository.All().FirstOrDefaultAsync(r => r.Id == ratingId);
            if (rating == null) return false;

            rating.IsCensored = true;

            this.reviewRepository.Update(rating);
            await this.reviewRepository.SaveChangesAsync();
            return true;
        }
        public async Task<List<RatingsViewModel>> GetAllRatingsAsync()
        {
            var ratings = await this.reviewRepository.All().Where(x => x.IsCensored == false).To<RatingsViewModel>().ToListAsync() ;

            return ratings;
        }
        public async Task<bool> ExistingRatingAsync(string wteId)
        {
            var result = await this.reviewRepository.All().AnyAsync(x=>x.WteId == wteId);

            return result;
        }
    }
}
