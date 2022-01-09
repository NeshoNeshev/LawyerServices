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
            var count = this.requestRepository.All().Count();
            return count;
        }
        public IEnumerable<T> GetAllRequests<T>(int? count = null)
        {
            IQueryable<Request> query = this.requestRepository.All().Where(r=>r.IsApproved == false);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}