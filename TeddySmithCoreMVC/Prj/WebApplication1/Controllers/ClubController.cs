using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository m_clubRepository;
        private readonly IPhotoService m_photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            m_clubRepository= clubRepository;
            m_photoService= photoService;
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
        public async Task<IActionResult> Create(CreateClubViewModel clubViewModel)
        {
            if(ModelState.IsValid)
            {
                var result = await m_photoService.AddPhotoAsync(clubViewModel.Image);

                var club = new Club
                {
                    Title = clubViewModel.Title,
                    Description = clubViewModel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = clubViewModel.Address.City,
                        State = clubViewModel.Address.State,
                        Street= clubViewModel.Address.Street
                    }
                };
                m_clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubViewModel);
        }
    }
}
