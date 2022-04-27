namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class EditLawFirmAdministrationModel
    {
        public string Id { get; set; }

        public string? Address { get; set; }

        public string? About { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ImgUrl { get; set; }

        public string? WebSiteUrl { get; set; }

        public string Email { get; set; }

        public string? FacebookUrl { get; set; }

        public string? LinkedinUrl { get; set; }

        public bool PhoneVerification { get; set; }
    }
}
