using System;
using System.Numerics;

namespace Caven.Common.Windows
{
    /// <summary>
    /// Gets t
    /// </summary>
    class BlackmanHarrisWindow : IWindowFunction
    {
        /// <summary>
        /// Gets the Blackman-Harris window.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public Complex[] GetWindow(int length)
        {
            var array = new Complex[length];

            // Hanning window
            for (var i = 0; i < length; i++)
            {
                array[i] = 0.355768f - 0.487396f * Math.Cos(2 * Math.PI * i / (length - 1))
                           + 0.144232 * Math.Cos(4 * Math.PI * i / (length - 1))
                           - 0.012604 * Math.Cos(6 * Math.PI * i / (length - 1));
            }
            return array;
        }
    }
}
