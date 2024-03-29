﻿using Duende.IdentityServer.EntityFramework.Options;
using LawyerServices.Data.Models;
using LawyerServices.Data.Models.SystemModels;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace LawyerServices.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>//: IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
           typeof(ApplicationDbContext).GetMethod(
               nameof(SetIsDeletedQueryFilter),
               BindingFlags.NonPublic | BindingFlags.Static);


        public ApplicationDbContext(
           DbContextOptions options,
           IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}
        //private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
        //   typeof(ApplicationDbContext).GetMethod(
        //       nameof(SetIsDeletedQueryFilter),
        //       BindingFlags.NonPublic | BindingFlags.Static);


        ////public ApplicationDbContext(
        ////   DbContextOptions options,
        ////   IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        ////{
        ////}
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}
        public DbSet<Country> Countries { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Court> Courts { get; set; }

        public DbSet<AreasOfActivity> AreasOfActivities { get; set; }

        public DbSet<AreasCompany> AreasCompanies { get; set; }

        public DbSet<WorkingTimeException> WorkingTimeExceptions { get; set; }

        public DbSet<WorkingTimeDays> WorkingTimeDays { get; set; }

        public DbSet<WorkingTime> WorkingTimes { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<FixedCostService> FixedCostServices { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<LawFirm> LawFirms { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            IndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletable).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletable
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
           
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<AreasCompany>().HasKey(sc => new { sc.CompanyId, sc.AreasOfActivityId });

            builder.Entity<AreasCompany>()
                .HasOne<Company>(sc => sc.Company)
                .WithMany(s => s.AreasCompanies)
                .HasForeignKey(sc => sc.CompanyId);


            builder.Entity<AreasCompany>()
                .HasOne<AreasOfActivity>(sc => sc.AreasOfActivity)
                .WithMany(s => s.AreasCompanies)
                .HasForeignKey(sc => sc.AreasOfActivityId);

        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IDateInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IDateInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
