using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sebs.Toolkit.JsonManagement.Validation
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/29/2023 8:56:58 AM
    /// </summary>
    public class JsonValidation
    {
        public static bool IsValid(string filePath)
        {
            return
                 IsPathValid(filePath) &&
                 IsDirectoryValid(filePath) &&
                 IsFileValid(filePath) &&
                 IsExtensionValid(filePath) &&
                 IsContentSignatureValid(filePath) &&
                 IsContentParsingValid(filePath);
        }

        private static bool IsPathValid(string filePath)
            => !string.IsNullOrWhiteSpace(filePath);

        private static bool IsDirectoryValid(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                var result = Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                return result.Exists;
            }
            return true;
        }

        private static bool IsFileValid(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var result = File.Create(filePath);
                result.Close();
                return File.Exists(filePath);
            }
            return true;
        }

        private static bool IsExtensionValid(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension == ".json";
        }

        private static bool IsContentSignatureValid(string filePath)
        {
            try
            {
                var jsonContent = File.ReadAllText(filePath).Trim();
                if (!(jsonContent.StartsWith("[") && jsonContent.EndsWith("]")))
                {
                    if (!(jsonContent.StartsWith("{") && jsonContent.EndsWith("}")))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return false;
            }
        }

        private static bool IsContentParsingValid(string filePath)
        {
            try
            {
                var jsonContent = File.ReadAllText(filePath).Trim();

                var obj = JToken.Parse(jsonContent);
                return true;
            }
            catch (JsonReaderException jex)
            {
                var exception = jex;
                return false;
            }
            catch (Exception ex) //other exception
            {
                var exception = ex;
                return false;
            }
        }

        private static bool IsJsonContentArray(string filePath)
        {
            try
            {
                var jsonContent = File.ReadAllText(filePath).Trim();
                if (jsonContent.StartsWith("[") && jsonContent.EndsWith("]"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return false;
            }
        }
    }
}
