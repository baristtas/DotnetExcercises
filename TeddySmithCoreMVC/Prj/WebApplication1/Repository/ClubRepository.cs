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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Club>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Club> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return true;
        }

        public bool Update(Club club)
        {
            throw new NotImplementedException();
        }
    }
}
