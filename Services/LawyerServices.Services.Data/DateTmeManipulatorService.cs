namespace LawyerServices.Services.Data
{
    public class DateTmeManipulatorService : IDateTmeManipulatorService
    {


        public DateTime ConvertStringToDateTime(string date)
        {
            DateTime formatDate;
            try
            {
                //dd/MM/yyyy HH:mm:ss

                //DateTime date = DateTime.ParseExact(dateStr, "MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                formatDate = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Wrong date");
            }

            var dateFinaly = formatDate.ToString("yyyy-MM-dd HH:mm:ss");
            var result = DateTime.ParseExact(dateFinaly, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return formatDate;
        }
    }
}
