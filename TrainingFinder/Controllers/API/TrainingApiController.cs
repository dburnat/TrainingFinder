using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Dtos.Training;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Entities;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Route("api/training")]
    [ApiController]
    public class TrainingApiController : ControllerBase
    {
        private readonly ITrainingRepository _trainingRepository;
        private IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trainingRepository"></param>
        public TrainingApiController(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _mapper = mapper;
            _trainingRepository = trainingRepository;
        }

        /// <summary>
        /// Gets all trainings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var trainings = _trainingRepository.Trainings;
                var model = _mapper.Map<IList<GetTrainingDtoWithoutUsers>>(trainings);
                return StatusCode(200, model);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Gets one training by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetTraining(int id)
        {
            try
            {
                var getTrainingResponse = _trainingRepository.GetById(id);
                if (getTrainingResponse.isStatusCodeSuccess())
                {
                    var model = _mapper.Map<GetTrainingDto>(getTrainingResponse.Data);
                    return StatusCode(getTrainingResponse.StatusCode, model);
                }
                else
                    return StatusCode(getTrainingResponse.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Return trainings in given range of time
        /// </summary>
        /// <param name="timeRangeFrom"></param>
        /// <param name="timeRangeTo"></param>
        /// <returns></returns>
        [Route("time/{timeRangeFrom}/{timeRangeTo}")]
        [HttpGet]
        public IActionResult GetTrainingByTime(DateTime timeRangeFrom, DateTime timeRangeTo)
        {
            try
            {
                var trainings =
                    _trainingRepository.Trainings.Where(x => x.DateTime >= timeRangeFrom && x.DateTime <= timeRangeTo);
                var model = _mapper.Map<IList<Training>>(trainings);

                return StatusCode(200, model);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Creates training
        /// </summary>
        /// <param name="trainingModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTraining([FromBody] TrainingModel trainingModel)
        {
            var training = _mapper.Map<Training>(trainingModel);
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
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Updates training
        /// </summary>
        /// <param name="trainingModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateTraining(int id, [FromBody] TrainingModel trainingModel)
        {
            var training = _mapper.Map<Training>(trainingModel);
            training.TrainingId = id;
            try
            {
                var getTrainingResponse = _trainingRepository.GetById(training.TrainingId);

                if (!getTrainingResponse.isStatusCodeSuccess() || getTrainingResponse.Data == null)
                    return NotFound();

                var updateResponse = _trainingRepository.Update(training);

                if (updateResponse.isStatusCodeSuccess() || updateResponse.Data != null)
                    return StatusCode(updateResponse.StatusCode, updateResponse.Data);
                else
                    return StatusCode(updateResponse.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Deletes training
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteTraining(int id)
        {
            try
            {
                var deleteResponse = _trainingRepository.Delete(id);

                return StatusCode(deleteResponse.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Returns list of trainings by body part
        /// </summary>
        /// <param name="bodyPart"></param>
        /// <returns></returns>
        [Route("bodyPart/{bodyPart}")]
        [HttpGet]
        public IActionResult GetByBodyPart(string bodyPart)
        {
            try
            {
                var trainings = _trainingRepository.Trainings.Where(x => x.Description.ToLower().Contains(bodyPart.ToLower()));
                var model = _mapper.Map<IList<GetTrainingDto>>(trainings);

                return StatusCode(200, model);
            }
            catch (Exception E)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }
    }
}