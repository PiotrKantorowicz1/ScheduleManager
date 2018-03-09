using System;

namespace Manager.Struct.Extensions
{
    public static class RandomExtensions
    {
        public static int CustomRandom(int nmb)
        {
            var random = new Random();
            var result = 0;

            if (nmb == 24)
            {
                result = random.Next(24);
            }

            result = random.Next(1, nmb + 1);

            return result;
        }
    }
}