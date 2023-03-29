using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

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

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if(!ModelState.IsValid)
            {
                //return View(club);
            }

            m_clubRepository.Add(club); 
            return RedirectToAction("Index");
        }
    }
}
