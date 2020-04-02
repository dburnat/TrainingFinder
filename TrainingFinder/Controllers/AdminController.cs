using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGymRepository _gymRepository;
        public AdminController(IGymRepository gymRepository)
        {
            _gymRepository = gymRepository;
        }
        public IActionResult List() => View(_gymRepository.Gyms);

        public IActionResult Create() => View("Edit", new Gym());
        public IActionResult Edit(int id)
        {
            var gym = _gymRepository.Gyms.FirstOrDefault(x => x.GymId == id);

            if (gym == null)
                return View("List");

            return View(gym);
        }


    }
}