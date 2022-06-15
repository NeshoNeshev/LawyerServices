namespace LawyerServices.Services.Data
{
    public interface ITimeService
    {
        public TimeSpan GetTimeOffset(DateTime date);
    }
}
