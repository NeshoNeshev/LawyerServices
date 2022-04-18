using GoogleMaps.LocationServices;

namespace LawyerServices.Services.Data.AdminServices
{
    public class LocationService : ILocationService
    {
        public double[] GetCoordinates(string address)
        {
           

            var locationService = new GoogleLocationService(apikey: "AIzaSyCMJSWEaQ3kuPiRGz6QKektakB55ZjQksQ");
           
            //var point = locationService.GetLatLongFromAddress();

            //var latitude = point.Latitude;
            //var longitude = point.Longitude;
            var array = new double[2];
            //array[0] = latitude;
            //array[1] = longitude;

            return array;
        }
    }
}
