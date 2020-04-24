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
        
        public class TrainingController : Controller
        {
        private readonly ITrainingRepository _trainingRepository;
        public TrainingController(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }
        /// <summary>
        /// Redirects to create training view
        /// </summary>
        /// <returns></returns>
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("Edit", new Training());
        }
        [HttpGet]
        public ViewResult List() => View(_trainingRepository.Trainings);

        [HttpPost("save")]
        public IActionResult Save(Training training)
        {
            if (!ModelState.IsValid || training == null)
            {
                ViewData["Message"] = "Given data is not valid!";
                return View("Index", _trainingRepository.Trainings);
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
            return View("List", _trainingRepository.Trainings);
        }

    }
}