namespace Sebs.Toolkit.RandomProvider.Services
{
    /// <summary>
    /// Purpose: Generate random strings.
    /// Created by: sebde
    /// Created at: 5/26/2023 11:26:33 PM
    /// </summary>
    public class RandomStringProvider : IRandomProvider<char>
    {
        private readonly int _length;
        private Random _generator;

        public RandomStringProvider(int length)
        {
            _length = length;
            _generator = new Random();
        }

        public IEnumerable<char> Generate()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = Enumerable.Repeat(chars, _length).Select(s => s[_generator.Next(s.Length)]).ToList();
            return result;
        }
    }
}
