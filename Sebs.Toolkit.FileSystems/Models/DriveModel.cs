namespace Sebs.Toolkit.FileSystems.Models
{
    public class DriveModel
    {
        /// <summary>
        /// (Drive/ File/ Folder)
        /// </summary>
        public DriveType ItemType { get; set; }

        /// <summary>
        /// The absolute path of the item
        /// </summary>
        public string? FullPath { get; set; }


        /// <summary>
        /// Get only the name of item from path
        /// </summary>
        public string? Name => ItemType == DriveType.Fixed ? this.FullPath : Path.GetFileNameWithoutExtension(this.FullPath);
    }
}
