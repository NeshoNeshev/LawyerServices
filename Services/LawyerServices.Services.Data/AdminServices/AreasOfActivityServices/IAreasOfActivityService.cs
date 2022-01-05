namespace LawyerServices.Services.Data.AdminServices.AreasOfActivityServices
{
    public interface IAreasOfActivityService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
