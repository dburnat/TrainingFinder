using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TrainingFinder.Data;
using TrainingFinder.Entities;

namespace TrainingFinder.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyDimensionApiController : ControllerBase
    {
        private readonly IBodyDimensionRepository _bodyDimensionRepository;

        public BodyDimensionApiController(IBodyDimensionRepository repo)
        {
            _bodyDimensionRepository = repo;
        }

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

        [Route("id/{id}")]
        [HttpGet]
        public IActionResult GetUsersBodyDimensions(User user)
        {
            try
            {
                var bodyDimensions = _bodyDimensionRepository.BodyDimensions.Where(x => x.User.Id == user.Id);

                return StatusCode(200, bodyDimensions);

            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }
         
    }
}