﻿using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Services.Data.AdminServices
{
    public class RequestsService : IRequestsService
    {
        private IDeletableEntityRepository<Request> requestRepository;
        public RequestsService(IDeletableEntityRepository<Request> requestRepository)
        {
            this.requestRepository = requestRepository;
        }
        public int GetRequestsCount()
        {
            var count = this.requestRepository.All().Where(x => x.IsApproved == false).Count();
            return count;
        }
        public IEnumerable<T> GetAllRequests<T>(int? count = null)
        {
            IQueryable<Request> query = this.requestRepository.All().Where(x => x.IsApproved == false);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
        public async Task SetIsApprovedAsync(string id)
        {
            var request = this.requestRepository.All().FirstOrDefault(r => r.Id == id);
            if (request != null)
            {
                request.IsApproved = true;
                this.requestRepository.Update(request);
                await this.requestRepository.SaveChangesAsync();
            }

        }
    }
}
