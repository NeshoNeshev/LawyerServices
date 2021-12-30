namespace LawyerServices.Services.Data
{
    public interface ICountryService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
