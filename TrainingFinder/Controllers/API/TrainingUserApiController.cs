using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Services.TrainingUserService;

namespace TrainingFinder.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingUserApiController : ControllerBase
    {
        private readonly ITrainingUserService _trainingUserService;
        public TrainingUserApiController(ITrainingUserService trainingUserService)
        {
            _trainingUserService = trainingUserService;
        }

        [HttpPost]
        public IActionResult Create(AddTrainingUserDto newTrainingUser)
        {
            var response = _trainingUserService.AddTrainingUser(newTrainingUser);
            return StatusCode(201, response);
        }
    }
}