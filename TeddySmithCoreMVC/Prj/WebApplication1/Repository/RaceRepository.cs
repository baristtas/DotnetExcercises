using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext m_context;

        public RaceRepository(ApplicationDbContext context)
        {
            m_context = context;
        }
        public bool Add(Race race)
        {
            m_context.Races.Add(race);  
            return Save();
        }

        public bool Delete(Race race)
        {
            m_context.Races.Remove(race);
            return Save(); 
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await m_context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await m_context.Races.FirstOrDefaultAsync(z=> z.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await m_context.Races.Where(a=> a.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var isSaved = m_context.SaveChanges();
            return isSaved > 0 ? true : false;
        }   

        public bool Update(Race race)
        {
            m_context.Update(race);
            return Save();
        }
    }
}
