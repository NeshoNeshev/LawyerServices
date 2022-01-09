﻿namespace LawyerServices.Services.Data.AdminServices
{
    public interface IRequestsService
    {
        public int GetRequestsCount();

        public IEnumerable<T> GetAllRequests<T>(int? count = null);

        public IEnumerable<T> SetIsApproved<T>(string id);
    }
}
