using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingApiController : ControllerBase
    {
        private readonly ITrainingRepository _trainingRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trainingRepository"></param>
        public TrainingApiController(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        /// <summary>
        /// Gets one training by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTraining(int id)
        {
            try
            {
                var getTrainingResponse = _trainingRepository.GetById(id);
                if (getTrainingResponse.isStatusCodeSuccess() || getTrainingResponse != null)
                    return StatusCode(getTrainingResponse.StatusCode);
                else
                    return StatusCode(getTrainingResponse.StatusCode);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Creates training
        /// </summary>
        /// <param name="training"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTraining(Training training)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createResult = _trainingRepository.Create(training);
                    return StatusCode(createResult.StatusCode);
                }
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Updates training
        /// </summary>
        /// <param name="training"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateTraining(Training training)
        {
            try
            {
                var getTrainingResponse = _trainingRepository.GetById(training.TrainingId);

                if (!getTrainingResponse.isStatusCodeSuccess() || getTrainingResponse.Data == null)
                    return NotFound();

                var updateResponse = _trainingRepository.Update(training);

                if (updateResponse.isStatusCodeSuccess() || updateResponse.Data != null)
                    return StatusCode(updateResponse.StatusCode);
                else
                    return StatusCode(updateResponse.StatusCode);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }
    }
}