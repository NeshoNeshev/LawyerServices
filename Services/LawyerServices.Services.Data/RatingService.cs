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
        private readonly IDeletableEntityRepository<Company> companyRerpository;

        public RatingService(IDeletableEntityRepository<Review> reviewRepository, IDeletableEntityRepository<Company> companyRerpository)
        {
            this.reviewRepository = reviewRepository;
            this.companyRerpository = companyRerpository;
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
            this.reviewRepository.HardDelete(rating);
            //this.reviewRepository.Update(rating);
            await this.reviewRepository.SaveChangesAsync();
            return true;
        }
        public async Task<List<RatingsViewModel>> GetAllRatingsAsync()
        {
            var ratings = await this.reviewRepository.All().Where(x => x.IsCensored == false && x.IsModerated == true).To<RatingsViewModel>().ToListAsync() ;

            return ratings;
        }
        public async Task ModerateRatingAsync(string ratingId)
        {
            var rating = await this.reviewRepository.All().FirstOrDefaultAsync(x => x.Id == ratingId);
            var lawyer = await this.companyRerpository.All().FirstOrDefaultAsync(c => c.Id == rating.CompanyId);
            if (rating == null || lawyer == null)
            {
                return;
            }
            try
            {
               
                lawyer.AverageGrade += ((rating.ServiceRating + rating.TrustworthyRating) / 2);
                rating.IsModerated = true;
                this.reviewRepository.Update(rating);
                await this.reviewRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Rating not moderate");
            }
           
        }
        public async Task<IEnumerable<RatingsViewModel>> GetAllModerateRatingsAsync()
        {
            var ratings = await this.reviewRepository.All().Where(x => x.IsModerated == false).To<RatingsViewModel>().ToListAsync();

            return ratings;
        }
        public async Task<bool> ExistingRatingAsync(string wteId)
        {
            var result = await this.reviewRepository.All().AnyAsync(x=>x.WteId == wteId);

            return result;
        }
    }
}
