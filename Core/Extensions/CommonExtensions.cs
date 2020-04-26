using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Extensions
{
    public static class CommonExtensions
    {
        public static bool In<T>(this T value, params T[] input)
        {
            return input.Any(n => object.Equals(n, value));
        }

    }
}
