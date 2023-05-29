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

        /// Return a random float between 0.0 (inclusive) and 1.0 (exclusive.)
        public float NextRandom() { return (float)_generator.NextDouble(); }

        /// Return a random float between 0.0 and range, inclusive from both ends.
        public float NextRandom(float range) { return (float)_generator.NextDouble() * range; }

        /// Return a random float between min and max, inclusive from both ends.
        public float NextRandom(float min, float max) { return (float)((_generator.NextDouble() * (max - min)) + min); }

        /// Return a random integer between min and max - 1.
        public int NextRandom(int min, int max) { return _generator.Next(min, max); }
    }
}
