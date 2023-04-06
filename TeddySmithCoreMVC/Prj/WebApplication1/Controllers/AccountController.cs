using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.ViewModels;
using WebApplication1.Data;
using WebApplication1.Models;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly SignInManager<AppUser> m_signInManager;
        private readonly ApplicationDbContext m_dbContext;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser>signInManager, ApplicationDbContext dbContext)
        {
            m_userManager= userManager;
            m_dbContext= dbContext;
            m_signInManager = signInManager;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel loginVM)
        {
            if(!ModelState.IsValid) return View(loginVM);

            var user = await m_userManager.FindByEmailAsync(loginVM.EmailAddress);

            if(user != null)
            {
                //User is found, check password
                var passwordCheck = await m_userManager.CheckPasswordAsync(user, loginVM.Password);

                if(passwordCheck)
                {
                    //Password correct. Sign in.
                    var result = await m_signInManager.PasswordSignInAsync(user,loginVM.Password,false,false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction ("Index", "Race");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong credientals. Please try again!";
                return View(loginVM);
            }
            //User not found.
            TempData["Error"] = "Wrong Credientals. Pls try again";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await m_userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "E-mail is already registered!";
                return View(registerVM);
                
            }
            var newUser = new AppUser
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await m_userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await m_userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return View("Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await m_signInManager.SignOutAsync();
            return RedirectToAction("Index","Race");
        }
    }
}
