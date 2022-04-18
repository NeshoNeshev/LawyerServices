namespace LawyerServices.Services.Data.AdminServices
{
    public interface ILocationService
    {
        public double[] GetCoordinates(string address);
    }
}
