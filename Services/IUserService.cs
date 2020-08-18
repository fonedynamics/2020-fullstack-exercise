using fonedynamics._2020.fullstack.exercise.Models;

namespace fonedynamics._2020.fullstack.exercise.Services
{
    public interface IUserService
    {
        bool Login(Login criteria);
    }
}