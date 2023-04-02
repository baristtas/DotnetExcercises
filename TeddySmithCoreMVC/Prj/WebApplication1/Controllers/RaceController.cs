using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository m_raceRepository;
        private readonly IPhotoService m_photoService;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            m_raceRepository = raceRepository;
            m_photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var uploadedImage = await m_photoService.AddPhotoAsync(raceVM.Image);

                Race RaceToAdd = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    RaceCategory = raceVM.RaceCategory,
                    Image = uploadedImage.Url.ToString(),

                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State
                    }
                };
                m_raceRepository.Add(RaceToAdd);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
