namespace Sebs.Toolkit.RandomProvider.Services
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/26/2023 11:25:42 PM
    /// </summary>
    public interface IRandomProvider<T>
    {
        public IEnumerable<T> Generate();
    }
}

