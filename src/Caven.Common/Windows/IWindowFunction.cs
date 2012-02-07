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
        ///   Gets the corresponding window function
        /// </summary>
        /// <param name = "length">Length of the window</param>
        /// <returns>Window function</returns>
        Complex[] GetWindow(int length);
    }
}
