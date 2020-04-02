using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fonedynamics._2020.fullstack.exercise.Helpers;
using fonedynamics._2020.fullstack.exercise.Models;
using fonedynamics._2020.fullstack.exercise.Options;
using fonedynamics._2020.fullstack.exercise.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YamlDotNet.RepresentationModel;

namespace fonedynamics._2020.fullstack.exercise.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        public YamlFileOptions _yamlFileOptions;

        #region Constructors and Destructors
        /// <summary>Initializes a new instance of the CustomerService class.</summary>
        /// <param name="logger">The logger used to log issues.</param>
        /// <param name="yamlFileOptions">The Yaml File Options from app settings.</param>
        public CustomerService(ILogger<CustomerService> logger, IOptionsMonitor<YamlFileOptions> yamlFileOptions)
        {
            _logger = logger;
            _yamlFileOptions = yamlFileOptions?.CurrentValue ?? throw new ArgumentNullException(nameof(yamlFileOptions));
        }
        #endregion

        // TODO: Repository to be added at the later stage

        #region Public Methods

        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            if (string.IsNullOrEmpty(_yamlFileOptions.FilePath))
            {
                return null;
            }

            var items = YamlFileUtility.ReadYamlFile(_yamlFileOptions.FilePath, "customers");
            foreach (var yamlNode in items)
            {
                var item = (YamlMappingNode)yamlNode;

                var customer = new Customer
                {
                    Id = YamlHelper.ConvertYamlScalarNodeToString(item, "id"),
                    Name = YamlHelper.ConvertYamlScalarNodeToString(item, "name"),
                    NumEmployees = Convert.ToInt16(YamlHelper.ConvertYamlScalarNodeToString(item, "num_employees")),
                    Tags = YamlHelper.ConvertYamlScalarNodeToStingList(item, "tags")

                };
                customers.Add(customer);
            }
            return customers;
        }

        #endregion

    }
}
