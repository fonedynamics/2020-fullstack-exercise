using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoneDynamics.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoneDynamics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        // GET: api/Customers
        [HttpGet]
        public IActionResult Get( int numOfEmployee, string searchKey, int skip, int take, string sortCol ="name", bool sortAsc = true)
        {
            int allRecordCount = 0;
            List<int> numberOfEmployeeFilters = new List<int>();
            var cust = CustomerRepository.GetCustomers(numOfEmployee, searchKey, skip, take, sortCol, sortAsc, out allRecordCount, out numberOfEmployeeFilters);
            return Ok(new {
            results = cust,
            totalItems = allRecordCount,
            displayed = cust.Count,
            numberOfEmployeeFilters = numberOfEmployeeFilters
            });
        }

       
    }
}
