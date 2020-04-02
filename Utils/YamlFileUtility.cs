
#region Using

using System.IO;
using YamlDotNet.RepresentationModel;

#endregion

namespace fonedynamics._2020.fullstack.exercise.Utils
{
    public class YamlFileUtility
    {
        #region Public Methods

        public static YamlSequenceNode ReadYamlFile(string filepath, string value)
        {
            var reader = new StreamReader(filepath);
            var yamlStream = new YamlStream();
            yamlStream.Load(reader);
            var mapping =
                (YamlMappingNode)yamlStream.Documents[0].RootNode;
            var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode(value)];
            return items;
        }

        #endregion

    }
}
