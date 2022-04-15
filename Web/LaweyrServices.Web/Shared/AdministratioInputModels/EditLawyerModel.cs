namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class EditLawyerModel
    {
        public string Id { get; set; }

        public string? Names { get; set; }

        public string? WebSite { get; set; }

        public string? Address { get; set; }

        public string? OfficeName { get; set; }

        public string? Languages { get; set; }

        public string? HeaderText { get; set; }

        public string? AboutText { get; set; }

        public bool PhoneVerification { get; set; }

        public string? ImgUrl { get; set; }
    }
}
