using IT.Employer.Domain;
using IT.Employer.Domain.Models.TeamN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Stores
{
    public class TeamStore : ITeamStore
    {
        private readonly ItEmployerDbContext _context;

        public TeamStore(ItEmployerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Team team)
        {
            await _context.AddAsync(team);
            await _context.SaveChangesAsync();

            return team.Id;
        }

        public async Task Delete(Guid id)
        {
            Team team = _context.Teams.Find(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public Team GetById(Guid id)
        {
            return _context.Teams
                .AsNoTracking()
                .Include(t => t.Company)
                .Include(t => t.Members)
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task Update(Team team)
        {
            _context.Update(team);
            await _context.SaveChangesAsync();
        }
    }
}
