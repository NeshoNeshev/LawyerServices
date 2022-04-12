using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ILawFirmService
    {
        public Task<string> CreateLawFirm(LawFirmInputModel model);

        public  Task EditLawFirmImage(byte[] bytes, string lawFirmId, string extension);

        public string GetIdByName(string name);

        public LawFirmViewModel GetLawFirm(string lawFirId);

        public IEnumerable<T> GetAll<T>(int? count = null);
    }
}
