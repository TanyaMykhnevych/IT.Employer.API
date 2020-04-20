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

            context.Database.ExecuteSqlRaw($@"
insert into Companies (Id, CreatedOn, ModifiedOn, Name, Size, Type, Email, Phone, Address, Website, Description) values
('713e2973-15e7-4a02-a7d1-f825bb0ef17b', GETDATE(), GETDATE(), 'IT Craft', 1, 0, 'hr@techs.com.ua', '123-45-78', 'Haharina ave', 'https://www.itcraft.com.ua/',
'The IT Craft company has been working on the IT market for more than 15 years. We base our work on the principles of openness, predictability and responsibility. We always accomplish all that was promised to our clients, employees or colleagues.')");

            context.Database.ExecuteSqlRaw($@"
insert into Vacancies (Id, CreatedOn, ModifiedOn, Name, Description, Position, Profession, PrimaryTechnology, ExperienceYears, Image)
Values
(NEWID( ), GETDATE(), GETDATE(), '.NET Developer', 'Candidate will be mostly focused on middle tier business logic that is used by Batch(SSIS) and Online processing of payments', 3, 1, 3, 3, 'https://kanglaonline.com/wp-content/uploads/2017/04/dotnet-developers.jpg'),
(NEWID( ), GETDATE(), GETDATE(), 'Project Manager', 'We are looking for an experienced project manager. As a project manager, you will be responsible for managing work through the application of knowledge, skills, tools, and techniques to project activities to meet the project requirements.', 4, 6, 19, 7, 'https://europeanmovement.eu/wp-content/uploads/2018/09/VACANDY.png'),
(NEWID( ), GETDATE(), GETDATE(), 'Python Developer', 'Piper Companies is currently looking for a Python Developer in Durham, North Carolina (NC) to join a support team tasked with securing internal resources for a leading technology company providing worldwide support for their products', 2, 3, 2, 1, 'https://i2.wp.com/invento.com.bd/wp-content/uploads/2019/07/python-developer.png');
");
        }
    }
}
