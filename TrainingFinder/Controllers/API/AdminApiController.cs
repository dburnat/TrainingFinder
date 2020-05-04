using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;
        private IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gymRepository"></param>
        public AdminApiController(IGymRepository gymRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gymRepository = gymRepository;
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

        /// <summary>
        /// Deletes gym from repository by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGym(int id)
        {
            try
            {
                var deleteResponse = _gymRepository.Delete(id);
                return StatusCode(deleteResponse.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Updates given gym
        /// </summary>
        /// <param name="gym"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateGym(Gym gym)
        {
            try
            {
                var updateResponse = _gymRepository.Update(gym);
                return StatusCode(updateResponse.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }
    }
}