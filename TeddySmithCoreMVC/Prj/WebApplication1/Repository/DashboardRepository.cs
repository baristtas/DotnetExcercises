using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Interfaces;
using WebApplication1.Interfaces;
using WebApplication1.Data;
using WebApplication1.Models;

namespace RunGroopWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext m_dbContext;
        private readonly IHttpContextAccessor m_httpContextAccessor;
        public DashboardRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            m_dbContext= dbContext;
            m_httpContextAccessor= httpContextAccessor;
        }


        public async Task<List<Club>> GetAllUserClubs()
        {
            var currUser = m_httpContextAccessor.HttpContext?.User;
            var userClubs = m_dbContext.Clubs.Where(r => r.AppUser.Id == currUser.ToString());

            return await userClubs.ToListAsync();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currUser = m_httpContextAccessor.HttpContext.User;
            var userRaces = m_dbContext.Races.Where(r=> r.AppUser.Id ==currUser.ToString());
            
            return await userRaces.ToListAsync();
        }
    }
}
