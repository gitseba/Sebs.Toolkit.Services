using Sebs.Toolkit.FileSystems.Models;
using System.Collections.ObjectModel;
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

        public async Task<IReadOnlyList<DriveModel>> GetFilesAsync(string fullPath, string extension)
        {
            var result = await Task.Run(() =>
            {
                var items = new List<DriveModel>();

                // Data structure to hold names of subfolders to be examined for files.
                Stack<string> directories = new Stack<string>(20);
                if (!Directory.Exists(fullPath))
                {
                    //throw new ArgumentException();
                }
                directories.Push(fullPath);

                while (directories.Count > 0)
                {
                    string currentDir = directories.Pop();
                    string[] subDirectories;
                    try
                    {
                        subDirectories = Directory.GetDirectories(currentDir);

                        string[]? files = null;
                        files = Directory.GetFiles(currentDir);

                        // Perform the required action on each file here.
                        // Modify this block to perform your required task.
                        foreach (string file in files)
                        {
                            try
                            {
                                Task.Delay(1);
                                // Perform whatever action is required in your scenario.
                                FileInfo fi = new(file);
                                if (fi.Extension.Contains(extension))
                                {
                                    items.AddRange(new ObservableCollection<DriveModel> { new DriveModel { FullPath = fi.FullName } });
                                }
                            }
                            catch (FileNotFoundException e)
                            {
                                // If file was deleted by a separate application or thread since the call to TraverseTree() then just continue.
                                continue;
                            }
                        }
                    }
                    // An UnauthorizedAccessException exception will be thrown if we do not have
                    // discovery permission on a folder or file. It may or may not be acceptable
                    // to ignore the exception and continue enumerating the remaining files and
                    // folders. It is also possible (but unlikely) that a DirectoryNotFound exception
                    // will be raised. This will happen if currentDir has been deleted by
                    // another application or thread after our call to Directory.Exists. The
                    // choice of which exceptions to catch depends entirely on the specific task
                    // you are intending to perform and also on how much you know with certainty
                    // about the systems on which this code will run.
                    catch (UnauthorizedAccessException ex)
                    {
                        continue;
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        continue;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                return items;
            });

            return result;
        }
    }
}
