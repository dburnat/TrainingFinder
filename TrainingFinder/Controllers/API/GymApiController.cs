using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GymApiController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gymRepository"></param>
        public GymApiController(IGymRepository gymRepository)
        {
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
        /// Creates gym
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateGym(Gym gym)
        {
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