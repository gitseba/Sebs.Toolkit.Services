namespace Sebs.FileSystems.Services
{
    /// <summary>
    /// Purpose: Interface for fetching files from PC System
    /// Created by: sebde
    /// Created at: 5/22/2023 10:57:13 PM
    /// </summary>
    public interface IFileService
    {
        IAsyncEnumerable<string> GetItemsAsync(string path, string patterns, CancellationToken cancellationToken = default);
    }
}

