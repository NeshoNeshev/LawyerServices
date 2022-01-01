using LawyerServices.Data.Models;

namespace LawyerServices.Data.Seeding
{
    internal class AreasOfActivitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.AreasOfActivities.Any())
            {
                return;
            }
            var listOfActivities = new List<AreasOfActivity>()
            {
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Авторско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Арбитраж" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Въздушно транспортно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Данъчно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Европейско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Етични правила" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Изборно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Имуществено право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Информационно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Концесии" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Медицинско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Морско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Недвижими имоти" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Обществени поръчки" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Право на Европейски съюз" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Процесуално представителство" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Сливания и придобивания" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Съдебни и административни производства" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Търговско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Френско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Административно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Банково право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Граждански процес" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Договорно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Екологично право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Застрахователно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Изпълнително производство" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Инвестиционно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Конкурентно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Корпоративно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Международно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Наказателно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Несъстоятелност" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Осигурително право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Приватизация и инвестиции" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Сделки с недвижими имоти" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Строително право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Съдебни спорове" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Фармацевтично право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Хазартно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Антимонополно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Вещно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Гражданско право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Дружествено право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Енергийно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Защита на човешките права" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Имиграционно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Интелектуална собственост" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Конституционно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Медийно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Митническо право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Наследствено право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Облигационно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Патентно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Процесуално право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Семейно право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Събиране на вземания" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Трудово право" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Финансово право" },
            };
            dbContext.AreasOfActivities.AddRange(listOfActivities);
            //var country = new AreasOfActivities() { Id = Guid.NewGuid().ToString(), Name = "България" };
            //var towns = new List<string>()
            //{"Благоевград","Бургас", "Варна","Велико Търново","Видин","Враца","Габрово","Добрич","Кърджали", "Кюстендил","Ловеч", "Монтана",
            //"Пазарджик", "Перник", "Плевен","Пловдив", "Разград", "Русе", "Силистра", "Сливен", "Смолян","София - град", "София - област",
            //"Стара Загора", "Търговище", "Хасково", "Шумен", "Ямбол"
            //};

            //foreach (var town in towns)
            //{
            //    country.Towns.Add(new Town { Id = Guid.NewGuid().ToString(), Name = town });
            //}

            //dbContext.Countries.Add(country);
        }
    }
}
