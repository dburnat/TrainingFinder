using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Entities;
using TrainingFinder.Helpers;
using TrainingFinder.Models;


namespace TrainingFinder.Controllers
{
    //TODO Edit and delete method
    [Authorize]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ViewResult Index() => View(_userRepository.GetAll());

        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                return View("Index", _userRepository.GetAll());

            return View(user.Data);
        }

        [HttpPost]
        public IActionResult Save(User user)
        {
            if (!ModelState.IsValid || user == null)
            {
                ViewData["message"] = "Given data is not valid!";
                return View("Edit", user);
            }

            try
            {
                _userRepository.Update(user);
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }

            return View("Index", _userRepository.GetAll());
        }
    }
}