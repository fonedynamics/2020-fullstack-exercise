using FoneDynamics.Models;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace FoneDynamics.Helpers
{

    public class CustomerRepository
    {
        private static List<Customer> _customer;
        private static void EnsureLoaded()
        {
            if (_customer == null)
            {
                _customer = ParseCustomer("exercise.yaml");
            }
        }
        protected static List<Customer> ParseCustomer(string filepath)
        {
            List<Customer> cust = new List<Customer>();
            string filetext = File.ReadAllText(filepath);
            var input = new StringReader(filetext);

            var yaml = new YamlStream();
            yaml.Load(input);

            var mapping = (yaml.Documents[0].RootNode as YamlMappingNode);

            var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("customers")];
            foreach (YamlMappingNode item in items)
            {
                var c = new Customer();
                c.Id = item.Children[new YamlScalarNode("id")].ToString();
                c.NumberOfEmployees = int.Parse(item.Children[new YamlScalarNode("num_employees")].ToString());
                c.Name = item.Children[new YamlScalarNode("name")].ToString();
                c.Tags = new List<string>();

                var tags = (YamlSequenceNode)item.Children[new YamlScalarNode("tags")];
                foreach (var tag in tags.Children)
                {
                    c.Tags.Add(tag.ToString());
                }
                cust.Add(c);
            }

            return cust;
        }

        public static List<Customer> GetCustomers(int numOfEmployee, string searchKey, int skip, int take, out int allRecordCount, out List<int> employeeFilters)
        {
            EnsureLoaded();

            allRecordCount = _customer.Count();
            employeeFilters = _customer.GroupBy(c => c.NumberOfEmployees).Select(c => c.FirstOrDefault()).Select(c => c.NumberOfEmployees).ToList();

            return _customer.Where(c =>
            (numOfEmployee > 0 ? c.NumberOfEmployees == numOfEmployee : true) &&
            (c.Name.Contains(searchKey ?? "") ||
            c.Tags.Any(x => x.Contains(searchKey ?? "")))).Skip(skip).Take(take).ToList();

        }
    }
}
