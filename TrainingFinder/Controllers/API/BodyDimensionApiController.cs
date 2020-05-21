using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TrainingFinder.Data;
using TrainingFinder.Dtos.BodyDimension;
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
                var model = _mapper.Map<IList<GetBodyDimensionsDtoWithoutUsers>>(bodyDimensions);

                return StatusCode(200, model);
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
        public IActionResult GetUsersBodyDimensions(int id)
        {
            try
            {

                var bodyDimensions = _bodyDimensionRepository.BodyDimensions.Where(x => x.User.Id == id);

                var model = _mapper.Map<IList<GetBodyDimensionsDtoWithoutUsers>>(bodyDimensions);

                return StatusCode(200, model);

            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected internal server error has occured.");
            }
        }

        /// <summary>
        /// Creates new body dimensions
        /// </summary>
        /// <param name="AddbodyDimensionDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] AddBodyDimensionsDto AddbodyDimensionDto, int userId)
        {
            try
            {
                var model = _mapper.Map<BodyDimension>(AddbodyDimensionDto);

                if (ModelState.IsValid)
                {
                    var response = _bodyDimensionRepository.Create(model, userId);
                    return StatusCode(response.StatusCode, _mapper.Map<GetBodyDimensionsDtoWithoutUsers>(model));
                }
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected internal server error has occured." + e);
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