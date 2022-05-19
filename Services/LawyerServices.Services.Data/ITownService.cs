using LawyerServices.Data.Models;

namespace LawyerServices.Services.Data
{
    public interface ITownService
    {
        public Task<IEnumerable<T>> GetAll<T>(int? count = null);
    }
}
