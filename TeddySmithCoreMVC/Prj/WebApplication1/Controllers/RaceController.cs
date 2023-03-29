using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository m_raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            m_raceRepository = raceRepository; 
        }
        public async Task<IActionResult> Index()
        {
            return View(await m_raceRepository.GetAll());
        }

        public async Task<IActionResult> Detail(int id)
        {
            Models.Race race = await m_raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                //return View(race);
            }

            m_raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
