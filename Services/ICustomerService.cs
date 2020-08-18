using System.Collections.Generic;
using fonedynamics._2020.fullstack.exercise.Models;

namespace fonedynamics._2020.fullstack.exercise.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
    }
}