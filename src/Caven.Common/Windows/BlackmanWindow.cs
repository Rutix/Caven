using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Caven.Common.Windows
{
    /// <summary>
    /// Blackman Window class
    /// </summary>
    class BlackmanWindow : IWindowFunction
    {
        /// <summary>
        /// Gets the corresponding window function
        /// </summary>
        /// <param name="length">Length of the window</param>
        /// <returns>
        /// Window function
        /// </returns>
        public Complex[] GetWindow(int length)
        {
            var array = new Complex[length];

            // Hamming window
            for (int i = 0; i < length; i++)
            {
                array[i] = 0.42f - 0.5f * (float) Math.Cos(2 * Math.PI * i / (length - 1))
                           + 0.08f * Math.Cos(4 * Math.PI * i / (length - 1));
            }
            return array;
        }
    }
}
