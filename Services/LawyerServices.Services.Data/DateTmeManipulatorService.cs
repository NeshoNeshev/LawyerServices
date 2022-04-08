namespace LawyerServices.Services.Data
{
    public class DateTmeManipulatorService : IDateTmeManipulatorService
    {


        public DateTime ConvertStringToDateTime(string date)
        {
            DateTime formatDate;
            try
            {
                formatDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm tt", null);
            }
            catch (Exception)
            {

                throw new InvalidOperationException("Wrong date");
            }

            return formatDate;
        }
    }
}
