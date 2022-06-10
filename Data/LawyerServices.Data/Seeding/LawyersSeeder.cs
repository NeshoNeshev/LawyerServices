using LawyerServices.Data.Models;
using LawyerServices.Data.Models.Enumerations;

namespace LawyerServices.Data.Seeding
{
    internal class LawyersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Companies.Any())
            {
                return;
            }
            bool FreeFirstAppointment = false;
            bool NoWinNoFee = true;
            bool FixedCost = false;
            string firstId = "2b2f36d7-9617-48eb-b039-04548cecc65f";
            string secondId = "b73f006e-c50f-4c4f-abd8-42b0e68ca8c8";
            string townId = "";
            for (int i = 0; i < 240; i++)
            {
                if (i % 2 == 0)
                {
                    FreeFirstAppointment = true;
                    NoWinNoFee = false;
                    FixedCost = true;
                    townId = firstId;
                }
                else
                {
                    townId = secondId;
                }
                var workingTime = new WorkingTime()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "hjhk",
                    IsActiv = true,
                };
                dbContext.WorkingTimes.Add(workingTime);
                var a = Profession.Lawyer;
                var laweyr = new Company()
                {
                    Id = Guid.NewGuid().ToString(),
                    Names = $"Иван Иванов{i}",
                    TownId = townId,
                    Address = "sdkjas",
                    AverageGrade = 0,
                    ExpirationDate = DateTime.Now.AddDays(1),
                    FreeFirstAppointment = FreeFirstAppointment,
                    NoWinNoFee = NoWinNoFee,
                    FixedCost = FixedCost,
                    WorkingTimeId = workingTime.Id,
                    Profession = a,
                    StopAccount = false,
                };
                dbContext.Companies.Add(laweyr);
            }
        }
    }
}
