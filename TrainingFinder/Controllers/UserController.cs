using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainingFinder.Data;
using TrainingFinder.Models.Users;

namespace TrainingFinder.Controllers
{
    //TODO Edit and delete method
    [Authorize]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ViewResult Index() => View(_userRepository.GetAll());
    }
}