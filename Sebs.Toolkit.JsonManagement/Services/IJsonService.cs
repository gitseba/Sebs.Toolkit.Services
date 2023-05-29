namespace Sebs.Toolkit.JsonManagement.Services
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/29/2023 9:01:11 AM
    /// </summary>
    public interface IJsonService
    {
        bool WriteToJsonFileAsync<T>(string storagePath, T data);

        List<T> ReadFromJsonFile<T>(string storagePath);
    }
}
