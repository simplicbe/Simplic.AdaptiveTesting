using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// Time measuring for a test case
    /// </summary>
    public class TestDurationIndicator : Indicator
    {
        #region Private Methods
        private Stopwatch stopwatch;
        #endregion

        #region Public Methods
        /// <summary>
        /// Get the elapsed milliseconds
        /// </summary>
        /// <returns>Ellapsed milliseconds as long</returns>
        public override object GetResult()
        {
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Start timer
        /// </summary>
        public override void Start()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        /// <summary>
        /// Stop timer
        /// </summary>
        public override void Stop()
        {
            stopwatch.Start();
        }
        #endregion
    }
}
