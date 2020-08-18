using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoneDynamics.Models
{
    public class Customer
    {
        public string Id { get; internal set; }
        public int NumberOfEmployees { get; internal set; }
        public string Name { get; internal set; }
        public List<string> Tags { get; internal set; }
    }
}
