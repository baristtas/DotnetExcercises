using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.ViewModels;
using WebApplication1.Models;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository m_dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository) 
        {
            m_dashboardRepository= dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Club> clubs = await m_dashboardRepository.GetAllUserClubs();
            List<Race> races = await m_dashboardRepository.GetAllUserRaces();

            var dashboardVM = new DashboardViewModel
            {
                UserClubs = clubs,
                UserRaces = races
            };

            return View(dashboardVM);
        }
    }
}
