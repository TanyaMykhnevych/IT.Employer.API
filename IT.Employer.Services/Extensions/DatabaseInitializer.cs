using IT.Employer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IT.Employer.Services.Extensions
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabaseInitialized(ItEmployerDbContext context)
        {
            Boolean isFirstLaunch = !(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
            context.Database.Migrate();
            if (isFirstLaunch)
            {
                AddTestData(context);
            }
            context.Dispose();
        }

        public static void AddTestData(ItEmployerDbContext context)
        {
            AddIdentityData(context);
        }

        public static void AddIdentityData(ItEmployerDbContext context)
        {
            // password for superuser: 123456
            context.Database.ExecuteSqlRaw($@"
                    INSERT INTO aspnetusers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, FirstName, LastName, IsActive, Role) VALUES 
                    (NEWID() , 'superuser@gmail.com', 'SUPERUSER@GMAIL.COM', 'superuser@gmail.com', 'SUPERUSER@GMAIL.COM', 'true', 'AQAAAAEAACcQAAAAEIUEA9TSyuVATZj+snuxTMC4ZKk/jIcgJwa1M/aY/SBH1b9W4Nn7qMwLBTebRBvboA==', '123456789', 'true', 'false', 'false', '0', 'Super', 'User', 'true', 'Admin');");
        }
    }
}
