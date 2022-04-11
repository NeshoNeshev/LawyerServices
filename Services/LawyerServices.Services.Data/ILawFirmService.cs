using LaweyrServices.Web.Shared.LawFirmModels;

namespace LawyerServices.Services.Data
{
    public interface ILawFirmService
    {
        public Task CreateLawFirm(LawFirmInputModel model);

        public  Task EditLawFirmImage(byte[] bytes, string lawFirmId, string extension);
    }
}
