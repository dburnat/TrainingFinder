using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Models.Users;
using TrainingFinder.Models.ViewModels;
using TrainingFinder.Data;
using TrainingFinder.Models;
using TrainingFinder.Services.GymLocationService;


namespace TrainingFinder.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly SignInManager<AdminUser> _signInManager;
        private readonly UserManager<AdminUser> _userManager;
        private readonly IGymRepository _gymRepository;
        private readonly IGymLocationService _gymLocationService;

        public AdminController(SignInManager<AdminUser> signInManager, UserManager<AdminUser> userManager,
            IGymRepository gymRepository, IGymLocationService gymLocationService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _gymRepository = gymRepository;
            _gymLocationService = gymLocationService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AdminUser {UserName = model.UserName, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel {ReturnUrl = returnUrl};
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }


        [HttpGet]
        public IActionResult List() => View(_gymRepository.Gyms.Where(c => c.IsAddedByUser == false));

        [HttpGet]
        public IActionResult Create() => View("Edit", new Gym());

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gym = _gymRepository.Gyms.FirstOrDefault(x => x.GymId == id);
            
            if (gym == null)
                return View("List");

            var address = gym.Street + " " + gym.Number + ", " + gym.City;
            ViewBag.Latitude = _gymLocationService.GetLatitude(address);
            ViewBag.Longitude = _gymLocationService.GetLongitude(address);
            return View(gym);
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            
                return View(_gymRepository.Gyms.Where(x => x.IsAddedByUser == true));
        }

        [HttpPost]
        public IActionResult Save(Gym gym)
        {
            if (!ModelState.IsValid || gym == null)
            {
                ViewData["message"] = "Given data is not valid!";
                return View("Edit", gym);
            }
            else
            {
                try
                {
                    gym.IsAddedByUser = false;
                    var result = _gymRepository.SaveGym(gym);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ViewData["Message"] = "Gym could not be added to the database.";
                }

                return View("List", _gymRepository.Gyms);
            }
        }

        /// <summary>
        /// Deletes gym
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var gymToDelete = _gymRepository.Gyms.SingleOrDefault(x => x.GymId == id);

            if (gymToDelete != null)
            {
                try
                {
                    var result = _gymRepository.DeleteGym(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ViewData["Message"] = "Gym could not be deleted from the database.";
                }

                return View("list", _gymRepository.Gyms);
            }

            return View("List", _gymRepository.Gyms);
        }
        
        
    }
}