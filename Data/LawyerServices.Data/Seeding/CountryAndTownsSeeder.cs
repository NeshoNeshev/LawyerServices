using LawyerServices.Data.Models;

namespace LawyerServices.Data.Seeding
{
    internal class CountryAndTownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Countries.Any())
            {
                return;
            }

            var country = new Country() { Id = Guid.NewGuid().ToString(), Name = "България" };
            var towns = new List<string>()
            {"Благоевград","Бургас", "Варна","Велико Търново","Видин","Враца","Габрово","Добрич","Кърджали", "Кюстендил","Ловеч", "Монтана",
            "Пазарджик", "Перник", "Плевен","Пловдив", "Разград", "Русе", "Силистра", "Сливен", "Смолян","София - град", "София - област",
            "Стара Загора", "Търговище", "Хасково", "Шумен", "Ямбол"
            };

            foreach (var town in towns)
            {
                country.Towns.Add(new Town { Id = Guid.NewGuid().ToString(), Name = town });
            }

            dbContext.Countries.Add(country);
        }
    }
}
