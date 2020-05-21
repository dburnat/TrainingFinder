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

        /// <summary>
        /// Assign user to training
        /// </summary>
        /// <param name="newTrainingUser"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(AddTrainingUserDto newTrainingUser)
        {
            var response = _trainingUserService.AddTrainingUser(newTrainingUser);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Get user trainings by user's id
        /// </summary>
        /// <param name="id">Integer value of user's id</param>
        /// <returns>List of trainings assigned to user</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserTrainings(int id)
        {
            var response = _trainingUserService.GetUserTrainings(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}