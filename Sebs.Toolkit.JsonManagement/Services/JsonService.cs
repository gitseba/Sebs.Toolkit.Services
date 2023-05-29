using Newtonsoft.Json;
using Sebs.Toolkit.JsonManagement.Validation;

namespace Sebs.Toolkit.JsonManagement.Services
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/29/2023 9:02:12 AM
    /// </summary>
    public class JsonService : IJsonService
    {
        public List<T> ReadFromJsonFile<T>(string storagePath)
        {
            if (!JsonValidation.IsValid(storagePath))
            {
                return default;
            }

            // Read existing json data
            var existingJsonData = File.ReadAllText(storagePath);
            var registrations = JsonConvert.DeserializeObject<List<T>>(existingJsonData) ?? new List<T>();

            return registrations;
        }

        public bool WriteToJsonFileAsync<T>(string storagePath, T input)
        {
            if (!JsonValidation.IsValid(storagePath) || input == null)
            {
                return false;
            }

            var textInFile = File.ReadAllText(storagePath);
            if (textInFile.Length > 0)
            {
                var deserialized = JsonConvert.DeserializeObject<T>(textInFile);
            }

            string json = JsonConvert.SerializeObject(input, Formatting.Indented);
            File.WriteAllText(storagePath, json);

            return true;
        }
    }
}
