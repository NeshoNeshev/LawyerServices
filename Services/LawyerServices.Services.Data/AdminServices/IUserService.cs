namespace LawyerServices.Services.Data.AdminServices
{
    public interface IUserService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
