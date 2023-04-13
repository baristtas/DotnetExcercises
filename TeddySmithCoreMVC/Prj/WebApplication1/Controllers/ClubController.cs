using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor m_httpContextAccessor;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            m_clubRepository = clubRepository;
            m_photoService = photoService;
            m_httpContextAccessor = httpContextAccessor;
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
            if(User.Identity.IsAuthenticated)
            {
                var currUserId = User.GetUserId();

                var createClubViewModel = new CreateClubViewModel
                {
                    AppUserId = currUserId
                };
                return View(createClubViewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await m_photoService.AddPhotoAsync(clubViewModel.Image);

                var club = new Club
                {
                    Title = clubViewModel.Title,
                    Description = clubViewModel.Description,
                    Image = result.Url.ToString(),
                    AppUserId = m_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Address = new Address
                    {
                        City = clubViewModel.Address.City,
                        State = clubViewModel.Address.State,
                        Street = clubViewModel.Address.Street
                    },
                    ClubCategory = clubViewModel.ClubCategory

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

        public async Task<IActionResult> Edit(int id)
        {
            var ClubToEdit = await m_clubRepository.GetByIdAsync(id);
            if (ClubToEdit == null) return View("Error");

            var clubVM = new EditClubViewModel
            {
                Title = ClubToEdit.Title,
                Description = ClubToEdit.Description,
                AddressId = ClubToEdit.Address.id,
                Address = ClubToEdit.Address,
                ClubCategory = ClubToEdit.ClubCategory,
                URL = ClubToEdit.Image
            };

            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVMToEdit)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVMToEdit);
            }
            var userClub = await m_clubRepository.GetByIdAsync(id);

            if (userClub != null)
            {
                try
                {
                    await m_photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Couldnt delete previously uploaded photo!");
                    return View(clubVMToEdit);
                }

                var newImage = await m_photoService.AddPhotoAsync(clubVMToEdit.Image);


                userClub.ClubCategory = clubVMToEdit.ClubCategory;
                userClub.Address = clubVMToEdit.Address;
                userClub.AddressId = clubVMToEdit.AddressId;
                userClub.Image = newImage.Url.ToString();
                userClub.Title = clubVMToEdit.Title;
                userClub.Description = clubVMToEdit.Description;
                userClub.Id = clubVMToEdit.Id;

                m_clubRepository.Update(userClub);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await m_clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");

            return View(clubDetails);
        }

        [HttpPost, ActionName("DeleteClub")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubToDelete = await m_clubRepository.GetByIdAsync(id);
            if (clubToDelete == null) return View("Error");
            m_clubRepository.Delete(clubToDelete);

            return RedirectToAction("Index");
        }
    }
}
