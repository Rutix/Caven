using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Caven.Common.Windows
{
    public interface IWindowFunction
    {
        /// <summary>
        ///   Window the outer space in place
        /// </summary>
        /// <param name = "outerspace">Array to be windowed</param>
        /// <param name = "windowLength">Window length</param>
        void WindowInPlace(float[] outerspace, int windowLength);

        /// <summary>
        ///   Window the outer space in place
        /// </summary>
        /// <param name = "outerspace">Array to be windowed</param>
        /// <param name = "windowLength">Window length</param>
        void WindowInPlace(Complex[] outerspace, int windowLength);

        /// <summary>
        ///   Gets the corresponding window function
        /// </summary>
        /// <param name = "length">Length of the window</param>
        /// <returns>Window function</returns>
        Complex[] GetWindow(int length);
    }
}
