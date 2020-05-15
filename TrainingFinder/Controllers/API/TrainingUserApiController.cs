using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Services.TrainingUserService;

namespace TrainingFinder.Controllers.API
{
    [Route("api/traininguser")]
    [ApiController]
    public class TrainingUserApiController : ControllerBase
    {
        private readonly ITrainingUserService _trainingUserService;
        private readonly IUserRepository _userRepository;

        public TrainingUserApiController(ITrainingUserService trainingUserService, IUserRepository userRepository)
        {
            _trainingUserService = trainingUserService;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Create(AddTrainingUserDto newTrainingUser)
        {
            var response = _trainingUserService.AddTrainingUser(newTrainingUser);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserTrainings(int id)
        {
            var response = _trainingUserService.GetUserTrainings(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}