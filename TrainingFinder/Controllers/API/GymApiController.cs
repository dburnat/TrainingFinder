using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Authorize]
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
                var gyms = _gymRepository.Gyms.ToList();
                var model = _mapper.Map<IList<Gym>>(gyms);
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
        [HttpGet("{id}")]
        public IActionResult GetGymById(int id)
        {
            try
            {
                var gym = _gymRepository.Gyms.FirstOrDefault(x => x.GymId == id);
                return StatusCode(200);
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
        [HttpGet("{city}")]
        public IActionResult GetGymsByCity(string city)
        {
            try
            {
                var gyms = _gymRepository.Gyms.Where(x => x.City.ToLower() == city.ToLower()).ToList();
                return StatusCode(200);
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