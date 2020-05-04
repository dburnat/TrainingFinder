using System;
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
        /// 
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
                return StatusCode(200);
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
        [HttpGet]
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
                    return StatusCode(createResponse.StatusCode);
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