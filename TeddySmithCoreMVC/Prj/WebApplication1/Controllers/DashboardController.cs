using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext m_DbContext;
        public DashboardController(ApplicationDbContext context) 
        {
            m_DbContext= context;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
