#region Using

using System;
using fonedynamics._2020.fullstack.exercise.Helpers;
using fonedynamics._2020.fullstack.exercise.Models;
using fonedynamics._2020.fullstack.exercise.Options;
using fonedynamics._2020.fullstack.exercise.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YamlDotNet.RepresentationModel;

#endregion


namespace fonedynamics._2020.fullstack.exercise.Services
{
    public class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;
        public YamlFileOptions _yamlFileOptions;

        #region Constructors and Destructors
        /// <summary>Initializes a new instance of the UserService class.</summary>
        /// <param name="logger">The logger used to log issues.</param>
        /// <param name="yamlFileOptions">The Yaml File Options from app settings.</param>
        public UserService(ILogger<UserService> logger, IOptionsMonitor<YamlFileOptions> yamlFileOptions)
        {
            _logger = logger;
            _yamlFileOptions = yamlFileOptions?.CurrentValue ?? throw new ArgumentNullException(nameof(yamlFileOptions));
        }
        #endregion

        // TODO: Repository to be added at the later stage

        #region Public Methods

        public bool Login(Login criteria)
        {
            if (string.IsNullOrEmpty(_yamlFileOptions.FilePath))
            {
                return false;
            }

            var items = YamlFileUtility.ReadYamlFile(_yamlFileOptions.FilePath, "auth");

            foreach (var yamlNode in items)
            {
                var item = (YamlMappingNode)yamlNode;
                if (YamlHelper.ConvertYamlScalarNodeToString(item, "password") == criteria.Password &&
                    YamlHelper.ConvertYamlScalarNodeToString(item, "username") == criteria.Username)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}
