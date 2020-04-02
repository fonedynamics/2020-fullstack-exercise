#region Using

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fonedynamics._2020.fullstack.exercise.Models;
using fonedynamics._2020.fullstack.exercise.Services;

#endregion

namespace fonedynamics._2020.fullstack.exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        #region Constructors and Destructors
        /// <summary>Initializes a new instance of the UserController class.</summary>
        /// <param name="logger">The logger used to log issues.</param>
        /// <param name="userService">The userService used to log in.</param>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        #endregion

        #region Public Methods

        /// <summary>Logs in the user.</summary>
        /// <param name="model">The criteria.</param>
        /// <returns>True or False</returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody]Login model)
        {
            var result = _userService.Login(model);
            return Ok(result);
        }

        #endregion

    }
}