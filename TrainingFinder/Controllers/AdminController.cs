using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers
{
    [Authorize]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IGymRepository _gymRepository;
        public AdminController(IGymRepository gymRepository)
        {
            _gymRepository = gymRepository;
        }
        [HttpGet]
        public IActionResult List() => View(_gymRepository.Gyms);
        [HttpGet("create")]
        public IActionResult Create() => View("Edit", new Gym());
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var gym = _gymRepository.Gyms.FirstOrDefault(x => x.GymId == id);

            if (gym == null)
                return View("List");

            return View(gym);
        }
        [HttpPost("save")]
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


    }
}