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
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Авторско право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Арбитраж",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Въздушно транспортно право",IsActiv = false },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Данъчно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Европейско право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Етични правила"  ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Изборно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Имуществено право"  ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Информационно право"  ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Концесии"  ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Медицинско право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Морско право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Недвижими имоти",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Обществени поръчки" ,IsActiv = false} ,
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Право на Европейски съюз" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Процесуално представителство",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Сливания и придобивания" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Съдебни и административни производства",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Търговско право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Френско право"  ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Административно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Банково право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Граждански процес" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Договорно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Екологично право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Застрахователно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Изпълнително производство",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Инвестиционно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Конкурентно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Корпоративно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Международно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Наказателно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Несъстоятелност" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Осигурително право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Приватизация и инвестиции",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Сделки с недвижими имоти",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Строително право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Съдебни спорове",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Фармацевтично право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Хазартно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Антимонополно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Вещно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Гражданско право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Дружествено право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Енергийно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Защита на човешките права",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Имиграционно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Интелектуална собственост" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Конституционно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Медийно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Митническо право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Наследствено право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Облигационно право",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Патентно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Процесуално право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Семейно право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Събиране на вземания",IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Трудово право" ,IsActiv = false},
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Финансово право" ,IsActiv = false},
            } ;
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
