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
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Gym")]
    public class GymController : Controller
    {
        private readonly IGymRepository _gymRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gymRepository"></param>
        public GymController(IGymRepository gymRepository)
        {
            _gymRepository = gymRepository;
        }

        /// <summary>
        /// Main view with gyms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List() => View(_gymRepository.Gyms);

        /// <summary>
        /// Redirects to create view
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Create() => View("Edit", new Gym());

        /// <summary>
        /// Saves or edits gym
        /// </summary>
        /// <param name="gym"></param>
        /// <returns></returns>
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