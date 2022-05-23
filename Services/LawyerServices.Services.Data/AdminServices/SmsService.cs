using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace LawyerServices.Services.Data.AdminServices
{
    public class SmsService : ISmsService
    {
        public SmsService()
        {

        }
        public void SendSms(string phoneNumber, string content)
        {
            var twilioAccountSid = "";
            var twilioAuthToken = "";
            var fromPhoneNumber = "+";
            var toPhoneNumber = phoneNumber;

            TwilioClient.Init(
                username: twilioAccountSid,
                password: twilioAuthToken
            );
            
           MessageResource.Create(
                from: fromPhoneNumber,
                to: toPhoneNumber,
                body: content
            );
        }
    }
}