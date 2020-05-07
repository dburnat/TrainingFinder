using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Services.TrainingUserService;

namespace TrainingFinder.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("trainingUser")]
    public class TrainingUserController : Controller
    {
        private readonly ITrainingUserService _trainingUserService;
        public TrainingUserController(ITrainingUserService trainingUserService)
        {
            _trainingUserService = trainingUserService;
        }
        [HttpPost]
        public IActionResult AddTrainingUser(AddTrainingUserDto newTrainingUser)
        {
            _trainingUserService.AddTrainingUser(newTrainingUser);
            return View("Index", newTrainingUser);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}