using System.Collections.Generic;
using _2020_fullstack_exercise.Models;

namespace fonedynamics._2020.fullstack.exercise.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
    }
}