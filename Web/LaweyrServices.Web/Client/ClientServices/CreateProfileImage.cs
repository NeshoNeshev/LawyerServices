namespace LaweyrServices.Web.Client.ClientServices
{
    public class CreateProfileImage : ICreateProfileImage
    {
       
        public string UploadProfileImage(byte[] bytes, string userName)
        {
   
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            using (var ms = new MemoryStream(bytes))
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }
            var imgUrl = $"/images/{userName}.png";

            return imgUrl;
        }

    }
}
