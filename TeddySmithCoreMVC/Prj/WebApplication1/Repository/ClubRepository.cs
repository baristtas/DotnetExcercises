using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext m_context;

        public ClubRepository(ApplicationDbContext context)
        {
            m_context = context;
        }
        public bool Add(Club club)
        {
            m_context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            m_context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await m_context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await m_context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await m_context.Clubs.Include(i => i.Address).Where(c => c.Address.City == city).ToListAsync();
        }

        public bool Save()
        {
            var isSaved = m_context.SaveChanges();
            return isSaved > 0 ? true: false;   
        }

        public bool Update(Club club)
        {
            m_context.Update(club);
            return Save();
        }    
    }
}
