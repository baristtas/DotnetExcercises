using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository m_clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            m_clubRepository= clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Club> clubs = await m_clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Models.Club club = await m_clubRepository.GetByIdAsync(id);
            return View(club);
        }

        //Test 3
        public IActionResult Create()
        {
            return View();  
        }
    }
}
