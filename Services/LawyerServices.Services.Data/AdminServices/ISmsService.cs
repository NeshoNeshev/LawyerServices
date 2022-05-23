namespace LawyerServices.Services.Data.AdminServices
{
    public interface ISmsService
    {
        public void SendSms(string phoneNumber, string content);
    }
}
