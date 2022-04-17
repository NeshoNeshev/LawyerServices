namespace LawyerServices.Services.Data
{
    public class DateTmeManipulatorService : IDateTmeManipulatorService
    {


        public DateTime ConvertStringToDateTime(string date)
        {
            DateTime formatDate;
            try
            {
                //DateTime date = DateTime.ParseExact(dateStr, "MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                formatDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Wrong date");
            }

            return formatDate;
        }
    }
}
