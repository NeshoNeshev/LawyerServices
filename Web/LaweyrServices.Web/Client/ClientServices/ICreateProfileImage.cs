namespace LaweyrServices.Web.Client.ClientServices
{
    public interface ICreateProfileImage
    {
        public string UploadProfileImage(byte[] bytes, string userName);
    }
}
