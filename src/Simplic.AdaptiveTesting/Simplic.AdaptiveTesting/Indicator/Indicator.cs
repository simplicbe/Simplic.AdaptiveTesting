using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// And indicator represents a value which will be measerd during test execution (test-case)
    /// </summary>
    public abstract class Indicator
    {
        /// <summary>
        /// Start measuring for the current indicator
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stop measuring for the current indicator
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Get the result of the indocator
        /// </summary>
        /// <returns></returns>
        public abstract object GetResult();

        /// <summary>
        /// Get the indicator as a string
        /// </summary>
        /// <returns>indicator result as string</returns>
        public abstract string ToString();
    }
}
