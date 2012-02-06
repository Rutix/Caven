using System;
using System.Numerics;

namespace Caven.Common
{
    class HanningWindow
    {
        /// <summary>
        /// Gets the hanning window fucntion.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public Complex[] GetWindow(int length)
        {
            var array = new Complex[length];

            // Hanning window
            for (int i = 0; i < length; i++)
            {
                array[i] = 0.5f * (1 - Math.Cos(2 * Math.PI * i / (length - 1)));
            }
            return array;
        }

    }
}