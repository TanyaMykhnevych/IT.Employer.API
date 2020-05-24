using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Domain.Models.Hiring;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Domain.Models.User;
using IT.Employer.Domain.Models.VacancyN;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace IT.Employer.Domain
{
    public class ItEmployerDbContext : IdentityDbContext<AppUser, ITEmployerRole, Guid>
    {
        public const string CreatedOn = "CreatedOn";

        public ItEmployerDbContext(DbContextOptions<ItEmployerDbContext> options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Hire> Hires { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureCreatedOnProperty(builder);
        }

        public override int SaveChanges()
        {
            SetUtcFormat();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetUtcFormat();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetUtcFormat()
        {
            IEnumerable<EntityEntry> modifiedEntries = ChangeTracker.Entries()
               .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            DateTime now = DateTime.UtcNow;
            foreach (EntityEntry entry in modifiedEntries)
            {
                BaseEntity entity = entry.Entity as BaseEntity;
                if (entity == null) { continue; }

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = now;
                }

                entity.ModifiedOn = now;
            }
        }


        private static void ConfigureCreatedOnProperty(ModelBuilder builder)
        {
            List<Type> modelTypes = typeof(ItEmployerDbContext).GetProperties()
                                     .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                                     .Select(x => x.PropertyType.GetGenericArguments().First())
                                     .ToList();

            foreach (Type modelType in modelTypes)
            {
                PropertyInfo key = modelType.GetProperties().FirstOrDefault(x => x.Name.Equals(CreatedOn));
                if (key == null) continue;

                builder.Entity(modelType)
                            .Property(key.Name)
                            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }
    }
}
