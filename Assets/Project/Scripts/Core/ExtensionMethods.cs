using System;
using System.Data.SqlTypes;

namespace Project.Scripts.Core
{
    public static class ArrayExtensionMethods
    {
        private static readonly Random Rnd = new Random();
        
        public static T Random<T>(this T[] array)
        {
            if (array == null || array.Length == 0) return default;
            var index = Rnd.Next(0, array.Length);
            return array[index];
        }
    }
}