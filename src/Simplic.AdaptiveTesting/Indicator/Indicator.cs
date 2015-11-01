using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// And indicator represents a value which will be measerd during test execution (test-case).
    /// Call-Order:
    /// 1. Prepare: Allocate every thing
    /// 2. Start: Start the measuring. Should execute very fast
    /// 3. Stop: Stop measuring. Should execute very fast
    /// 4. Free: Free all allocated resources
    /// 5. GetResult: Return the result. Can be called more than one time.
    /// Should always return the same result after stop.
    /// </summary>
    public abstract class Indicator
    {
        /// <summary>
        /// Prepare the indicator. In this method all resources should be allocated,
        /// not in the constructor.
        /// </summary>
        public abstract void Prepare();

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
        public abstract IIndicatorResult GetResult();

        /// <summary>
        /// Free all resources. No resource should be freed in the stop method.
        /// Start and stop should execute very fast
        /// </summary>
        public abstract void Free();

        /// <summary>
        /// Get the indicator as a string
        /// </summary>
        /// <returns>indicator result as string</returns>
        public abstract string ToString();
    }
}
