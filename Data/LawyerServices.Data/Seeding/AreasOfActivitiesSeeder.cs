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
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Авторско право", BindingName ="Copyright" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Арбитраж", BindingName ="Arbitration"  },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Въздушно транспортно право" , BindingName ="AirTransportLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Данъчно право" , BindingName ="Taxlaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Европейско право" , BindingName ="EuropeanRight" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Етични правила" , BindingName ="EthicalRules" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Изборно право" , BindingName ="Suffrage" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Имуществено право" , BindingName ="PropertyLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Информационно право" , BindingName ="InformationLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Концесии" , BindingName ="Concessions" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Медицинско право" , BindingName ="MedicalLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Морско право" , BindingName ="MaritimeLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Недвижими имоти" , BindingName ="RealEstate" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Обществени поръчки" , BindingName ="Procurement" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Право на Европейски съюз" , BindingName ="EuropeanUnionLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Процесуално представителство" , BindingName ="ProceduralRepresentation" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Сливания и придобивания" , BindingName ="MergersAcquisitions" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Съдебни и административни производства", BindingName ="JudicialAdministrativeProceedings" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Търговско право" , BindingName ="CommercialLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Френско право" , BindingName ="FrenchLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Административно право" , BindingName ="AdministrativeLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Банково право" , BindingName ="BankingLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Граждански процес" , BindingName ="CivilProceedings" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Договорно право" , BindingName ="ContractLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Екологично право" , BindingName ="EnvironmentalLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Застрахователно право" , BindingName ="InsuranceLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Изпълнително производство" , BindingName ="ExecutiveProduction" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Инвестиционно право" , BindingName ="InvestmentLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Конкурентно право" , BindingName ="CompetitionLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Корпоративно право" , BindingName ="CorporateLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Международно право" , BindingName ="InternationalLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Наказателно право" , BindingName ="CriminalLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Несъстоятелност" , BindingName ="Bankruptcy" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Осигурително право" , BindingName ="AssuranceLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Приватизация и инвестиции" , BindingName ="PrivatizationAndInvestment" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Сделки с недвижими имоти" , BindingName ="RealEstateTransactions" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Строително право" , BindingName ="ConstructionLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Съдебни спорове" , BindingName ="Litigation" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Фармацевтично право" , BindingName ="PharmaceuticalLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Хазартно право" , BindingName ="GamblingLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Антимонополно право" , BindingName ="AntitrustLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Вещно право" , BindingName ="RealLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Гражданско право" , BindingName ="CivilLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Дружествено право" , BindingName ="CompanyLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Енергийно право" , BindingName ="EnergyLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Защита на човешките права" , BindingName ="Phr" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Имиграционно право" , BindingName ="ImmigrationLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Интелектуална собственост" , BindingName ="IntellectualProperty" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Конституционно право" , BindingName ="ConstitutionalRight" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Медийно право" , BindingName ="MediaLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Митническо право" , BindingName ="CustomsLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Наследствено право" , BindingName ="InheritanceLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Облигационно право" , BindingName ="ContractualLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Патентно право" , BindingName ="PatentLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Процесуално право" , BindingName ="ProceduralLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Семейно право" , BindingName ="FamilyLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Събиране на вземания" , BindingName ="CollectionOfReceivables" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Трудово право" , BindingName ="LaborLaw" },
               new AreasOfActivity(){Id = Guid.NewGuid().ToString(), Name = "Финансово право" , BindingName ="FinancialLaw" },
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
