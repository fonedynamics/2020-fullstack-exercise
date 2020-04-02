#region Using

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

#endregion


namespace fonedynamics._2020.fullstack.exercise.Helpers
{
    public class YamlHelper
    {
        #region Public Methods

        public static string ConvertYamlScalarNodeToString(YamlMappingNode yamlMappingNode, string value)
        {
            return yamlMappingNode.Children[new YamlScalarNode(value)].ToString();
        }

        public static List<string> ConvertYamlScalarNodeToStingList(YamlMappingNode yamlMappingNode, string value)
        {
            var tags = yamlMappingNode.Children[new YamlScalarNode(value)];
            return (from object tag in (IEnumerable)tags select tag.ToString()).ToList();
        }

        #endregion

    }
}
