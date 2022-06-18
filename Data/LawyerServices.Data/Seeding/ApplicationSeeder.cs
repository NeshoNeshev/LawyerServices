namespace LawyerServices.Data.Seeding
{
    public class ApplicationSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }


            var seeders = new List<ISeeder>
            {
                new CountryAndTownsSeeder(),
                new AreasOfActivitiesSeeder(),
                new RolesSeeder(),
                new UserSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
