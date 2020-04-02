using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace fonedynamics._2020.fullstack.exercise.Models
{
    public class Customer
    {
        #region Properties

        /// <summary>Gets or sets the customer id.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the number of employees.</summary>
        public int NumEmployees { get; set; }

        /// <summary>Gets or sets the customer name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the tags.</summary>
        public List<string> Tags { get; set; }

        #endregion

    }
}
