using Sebs.Toolkit.FileSystems.Models;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath">Path where to search for files</param>
        /// <param name="extension"> = ".cs" </param>
        /// <returns></returns>
        Task<IReadOnlyList<DriveModel>> GetFilesAsync(string fullPath, string extension);
    }
}

