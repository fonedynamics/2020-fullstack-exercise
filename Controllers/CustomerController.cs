#region Using

using fonedynamics._2020.fullstack.exercise.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace fonedynamics._2020.fullstack.exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        //public YamlFileOptions _yamlFileOptions;
        public ICustomerService _customerService;

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the CustomerController class.</summary>
        /// <param name="logger">The logger used to log issues.</param>
        /// <param name="customerService">The customer service which returns customers.</param>
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            //_yamlFileOptions = yamlFileOptions;
            _customerService = customerService;
        }

        #endregion

        #region Public Methods

        /// <summary>Logs in the user.</summary>
        /// <param name="model">The criteria.</param>
        /// <returns>True or False</returns>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var result = _customerService.GetCustomers();
            //var c = new CustomerService();
            //var result= CustomerService.
            return Ok(result);
        }

        #endregion

    }
}