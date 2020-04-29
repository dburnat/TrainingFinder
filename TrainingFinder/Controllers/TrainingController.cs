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
    [Route("training")]
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _trainingRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trainingRepository"></param>
        public TrainingController(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        /// <summary>
        /// Redirects to create training view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.Training = new Training()
            {
                GymId = id
            };
            return View("Edit", new Training());
        }


        /// <summary>
        /// Redirects to view with training added to selected gym
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult List(int id) => View(_trainingRepository.Trainings.Where(x => x.GymId == id));

        /// <summary>
        /// Saves or edits training
        /// </summary>
        /// <param name="training"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(Training training)
        {
            if (!ModelState.IsValid || training == null)
            {
                ViewData["Message"] = "Given data is not valid!";
                return RedirectToAction("List", "Gym");
            }
            else
            {
                try
                {
                    var result = _trainingRepository.SaveTraining(training);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ViewData["Message"] = "Training could not be added to the database.";
                }
            }

            return RedirectToAction("List", "Gym");
        }
    }
}