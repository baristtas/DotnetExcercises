using Microsoft.AspNetCore.Mvc;
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
    }
}
