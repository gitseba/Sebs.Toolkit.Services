using Newtonsoft.Json;
using Sebs.Toolkit.JsonManagement.Validation;
using System.Reflection;

namespace Sebs.Toolkit.JsonManagement.Configs
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/29/2023 8:54:28 AM
    /// </summary>
    public static class JsonConfig
    {
        private static string _defaultFilePath = Assembly.GetExecutingAssembly().Location;
        private static string _defaultDirectoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Storage");

        public static void Config()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_defaultDirectoryPath)))
            {
                Directory.CreateDirectory(_defaultFilePath);
            }

            Registration();
        }

        private static void Registration()
        {
            if (JsonValidation.IsValid(_defaultFilePath))
            {
                return;
            }

            using var createdFileStream = File.CreateText(_defaultFilePath);
            JsonSerializer serializer = new JsonSerializer
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            var serializedRegistrations = JsonConvert.SerializeObject(new object[] { }, Formatting.Indented);
            createdFileStream.WriteLine(serializedRegistrations.ToString());
            createdFileStream.Close();
        }
    }
}