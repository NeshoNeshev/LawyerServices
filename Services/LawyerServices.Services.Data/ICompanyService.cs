﻿using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;

namespace LawyerServices.Services.Data
{
    public interface ICompanyService
    {
        public Task<string> CreateMoreInformationAsync(MoreInformationInputModel model, string userId);

        public IList<Appointment> GetAllAppointmentsByLawyerId(string lawyerId);

        public MoreInformationInputModel GetMoreInformation(string companyId);

        public Task ChangeName(string companyId, string Name);

        public Task SaveCompanyAppointmentsAsync(Appointment appointment, string userId);

        public IList<Appointment> GetAllAppointments(string userId);

        public Task EditAppointmentAsync(Appointment model);

        public int UsersCount(string userId);

        public int UsersTodayCount(string userId);

        public string GetCompanyId(string userId);

        public Task UpdateFeaturesAsync(FeaturesInputModel model, string lawyerid);

        public FeaturesInputModel GetFeatures(string lawyerid);

        public IList<AppointmentViewModel> GetAllAppointmentsByCurrentDate(string date, string lawyerId);

        public Task ActivateAccountAsync(string id);

        public Task StopAccountAsync(string id);

        public IEnumerable<T> GetLawyer<T>(string id);
    }
}
