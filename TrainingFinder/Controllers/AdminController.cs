using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;

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
        


    }
}