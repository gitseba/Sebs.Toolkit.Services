using System.Runtime.CompilerServices;

namespace Sebs.FileSystems.Services
{
    /// <summary>
    /// Purpose: Read system files ("mp3 | .pdf" etc)
    /// Created by: sebde
    /// Created at: 5/22/2023 10:58:20 PM
    /// </summary>
    public class FileService : IFileService
    {
        public async IAsyncEnumerable<string> GetItemsAsync(string path, string patterns,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            foreach (var pattern in patterns.Split('|'))
            {
                foreach (var file in Directory.EnumerateFiles(path, pattern,
                    new EnumerationOptions
                    {
                        IgnoreInaccessible = true,
                        RecurseSubdirectories = true,
                        ReturnSpecialDirectories = false
                    }))
                {
                    await Task.Delay(1, cancellationToken);
                    yield return file;
                }
            }
        }
    }
}
