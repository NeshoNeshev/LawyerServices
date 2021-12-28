using LawyerServices.Data.Models;

namespace LawyerServices.Services.Data
{
    public interface ITownService
    {
        public Task<IEnumerable<Town>> GetAll(string id);
    }
}
