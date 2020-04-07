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
 
    public class AuthRepository
    {
        private static List<Auth> _auths;
        private static void EnsureLoaded()
        {
            if (_auths == null)
            {
                _auths = ParseAuths("exercise.yaml");
            }
        }
        protected static List<Auth> ParseAuths(string filepath)
        {
            List<Auth> auths = new List<Auth>();
            string filetext = File.ReadAllText(filepath);
            var input = new StringReader(filetext);

            var yaml = new YamlStream();
            yaml.Load(input);

            var mapping = (yaml.Documents[0].RootNode as YamlMappingNode);

            var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("auth")];
            foreach (YamlMappingNode item in items)
            {
                var auth = new Auth();
                auth.Username = item.Children[new YamlScalarNode("username")].ToString();
                auth.Password = item.Children[new YamlScalarNode("password")].ToString();
                auths.Add(auth);
            }

            return auths;
        }

        public static bool Validate(string username, string password)
        {
            EnsureLoaded();
            bool hasMatch = _auths.Any(c => c.Username.Equals(username, StringComparison.InvariantCulture) && c.Password == password);
            return hasMatch;
        }

        
    }
}
