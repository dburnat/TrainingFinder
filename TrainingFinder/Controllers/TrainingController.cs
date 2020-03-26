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
        public ViewResult List() => View(_trainingRepository.Trainings);




    }
}