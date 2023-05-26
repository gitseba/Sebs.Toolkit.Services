using System;

namespace Sebs.Toolkit.RandomProvider.Services
{
    /// <summary>
    /// Purpose: Generate random int.
    /// Created by: sebde
    /// Created at: 5/26/2023 11:26:33 PM
    /// </summary>
    public class RandomIntegerProvider : IRandomProvider<int>
    {
        private readonly int _count;
        private readonly int _maxCount;
        private Random _generator;

        /// <summary>
        /// Constructor have the responsability of providing data and 
        /// make the setup(instanciate objects) for the behavior of the class to run properly.
        /// </summary>
        public RandomIntegerProvider(int count, int maxCount = 100)
        {
            this._count = count;
            _maxCount = maxCount < 100 ? maxCount : 100;
            _generator = new Random();
        }

        public IEnumerable<int> Generate()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _generator.Next(_maxCount);
            }
        }
    }
}
