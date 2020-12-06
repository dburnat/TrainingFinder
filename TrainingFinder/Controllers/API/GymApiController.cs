using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Dtos.Gym;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Route("api/gym")]
    [ApiController]
    public class GymApiController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;
        private IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gymRepository"></param>
        /// <param name="mapper"></param>
        public GymApiController(IGymRepository gymRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gymRepository = gymRepository;
        }

        /// <summary>
        /// Return list of gyms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGyms()
        {
            try
            {
                var gyms = _gymRepository.Gyms.Where( c => c.IsAddedByUser == false).ToList();
                foreach (var gym in gyms)
                {
                    var trainings = gym.Trainings.OrderBy(x => x.DateTime).ToList();
                    gym.Trainings = trainings;
                }
                var model = _mapper.Map<IList<GetGymDto>>(gyms);
                return StatusCode(200, model);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Return gym by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("id/{id}")]       
        [HttpGet]
        public IActionResult GymById(int id)
        {
            try
            {
                var gym = _gymRepository.Gyms.FirstOrDefault(x => x.GymId == id);
                gym.Trainings = gym.Trainings.OrderBy(c => c.DateTime).ToList();
                var model = _mapper.Map<GetGymDto>(gym);
                return StatusCode(200, model);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Returns gyms in current city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [Route("city/{city}")]
        [HttpGet]
        public IActionResult GymsByCity(string city)
        {
            try
            {
                var gyms = _gymRepository.Gyms.Where(x => x.City.ToLower() == city.ToLower()).ToList();
                var model = _mapper.Map<IList<GetGymDto>>(gyms);
                return StatusCode(200, model);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Creates gym
        /// </summary>
        /// <param name="gymModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateGym([FromBody] GymModel gymModel)
        {
            //map model to entity
            var gym = _mapper.Map<Gym>(gymModel);

            try
            {
                if (ModelState.IsValid)
                {
                    var createResponse = _gymRepository.Create(gym);
                    return StatusCode(createResponse.StatusCode, gym);
                }
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }
    }
}