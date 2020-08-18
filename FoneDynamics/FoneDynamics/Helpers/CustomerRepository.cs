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

        public static List<Customer> GetCustomers(int numOfEmployee, string searchKey, int skip, int take, string sortCol, bool sortAsc, out int allRecordCount, out List<int> employeeFilters)
        {
            EnsureLoaded();

            allRecordCount = _customer.Count();
            employeeFilters = _customer.GroupBy(c => c.NumberOfEmployees).Select(c => c.FirstOrDefault()).Select(c => c.NumberOfEmployees).ToList();

            var uncut = _customer.Where(c =>
            (numOfEmployee > 0 ? c.NumberOfEmployees == numOfEmployee : true) &&
           // (c.Name.Contains(searchKey ?? "") ||
            c.Tags.Any(x => x.Contains(searchKey ?? "")));

            if (sortAsc)
            {
                if(sortCol == "name") uncut = uncut.OrderBy(c => c.Name).ToList();
                if (sortCol == "numOfEmp") uncut = uncut.OrderBy(c => c.NumberOfEmployees).ToList();
                if (sortCol == "tags") uncut = uncut.OrderBy(c => c.Tags.OrderBy(child=> child).FirstOrDefault()).ToList();
            }
            else
            {
                if (sortCol == "name") uncut = uncut.OrderByDescending(c => c.Name).ToList();
                if (sortCol == "numOfEmp") uncut = uncut.OrderByDescending(c => c.NumberOfEmployees).ToList();
                if (sortCol == "tags") uncut = uncut.OrderByDescending(c => c.Tags.OrderBy(child => child).FirstOrDefault()).ToList();
            }
            
            var res = uncut.Skip(skip).Take(take).ToList();

            return res;

        }
    }
}
