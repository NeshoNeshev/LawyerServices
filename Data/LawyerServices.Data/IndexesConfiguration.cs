using LawyerServices.Data.Models.SystemModels;
using Microsoft.EntityFrameworkCore;

namespace LawyerServices.Data
{
    internal static class IndexesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            var deletableEntityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletable).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletable.IsDeleted));
            }
        }
    }
}
