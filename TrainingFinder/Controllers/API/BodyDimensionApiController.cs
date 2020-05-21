using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TrainingFinder.Data;
using TrainingFinder.Dtos.User;
using TrainingFinder.Entities;
using TrainingFinder.Models;

namespace TrainingFinder.Controllers.API
{
    [Route("api/bodyDimension")]
    [ApiController]
    public class BodyDimensionApiController : ControllerBase
    {
        private readonly IBodyDimensionRepository _bodyDimensionRepository;
        private readonly IMapper _mapper;

        public BodyDimensionApiController(IBodyDimensionRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _bodyDimensionRepository = repo;
        }

        /// <summary>
        /// Returns list of all body dimensions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBodyDimensions()
        {
            try
            {
                var bodyDimensions = _bodyDimensionRepository.BodyDimensions.ToList();

                return StatusCode(200, bodyDimensions);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Return user body dimensions
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [Route("id/{id}")]
        [HttpGet]
        public IActionResult GetUsersBodyDimensions([FromBody]GetUserDto userModel)
        {
            try
            {
                var user = _mapper.Map<User>(userModel);

                var bodyDimensions = _bodyDimensionRepository.BodyDimensions.Where(x => x.User.Id == user.Id);

                return StatusCode(200, bodyDimensions);

            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Creates new body dimensions
        /// </summary>
        /// <param name="bodyDimension"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(BodyDimension bodyDimension)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = _bodyDimensionRepository.Create(bodyDimension);
                    return StatusCode(response.StatusCode, bodyDimension);
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
        /// Deletes body dimensions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var bodyDimensions = _bodyDimensionRepository.BodyDimensions.SingleOrDefault(x => x.Id == id);

                if (bodyDimensions != null)
                {
                    var response = _bodyDimensionRepository.Delete(id);
                    return StatusCode(response.StatusCode, null);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }
         
    }
}