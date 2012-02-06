using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Caven.Common
{
    class HammingWindow
    {
        /// <summary>
        /// Gets the hamming window.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public Complex[] GetWindow(int length)
        {
            var array = new Complex[length];

            // Hamming window
            for (int i = 0; i < length; i++)
            {
                array[i] = 0.54f - 0.46f * (float) Math.Cos(2 * Math.PI * i / (length - 1));
            }
            return array;
        }
    }
}
