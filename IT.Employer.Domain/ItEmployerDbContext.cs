using IT.Employer.Domain.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace IT.Employer.Domain
{
    public class ItEmployerDbContext : IdentityDbContext<AppUser, ITEmployerRole, Guid>
    {
        public ItEmployerDbContext(DbContextOptions<ItEmployerDbContext> options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
