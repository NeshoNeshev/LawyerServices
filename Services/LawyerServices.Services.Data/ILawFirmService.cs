﻿using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ILawFirmService
    {
        public Task<string> CreateLawFirmAsync(LawFirmInputModel model);

        public  Task EditLawFirmImageAsync(byte[] bytes, string lawFirmId, string extension);

        public Task<string> GetLawFirmIdAsync(string lawyerId);


        public string GetIdByName(string name);

        public Task<LawFirmViewModel> GetLawFirmByLawyerId(string lawyerId);

        public Task<LawFirmViewModel> GetLawFirm(string lawFirId);

        public LawFirmViewModel FindLawFirmByName(string lawFirmName);

        public Task<IEnumerable<T>> GetAll<T>(int? count = null);

        public Task EditLawFirmAsync(EditLawFirmAdministrationModel model);
    }
}
