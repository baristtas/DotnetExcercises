using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            List<Models.Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        public IActionResult Detail(int id)
        {
            Models.Club club = _context.Clubs.Include(a=> a.Address).FirstOrDefault(c => c.Id == id);
            return View(club);
        }
    }
}
