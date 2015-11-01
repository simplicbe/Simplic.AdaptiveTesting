using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.PlugIn.Std
{
    /// <summary>
    /// Indicator for measuring the duration of a test case
    /// </summary>
    [Simplic.AdaptiveTesting.PlugIns.IndicatorDefinition("duration", typeof(DurationIndicator))]
    public class DurationIndicator : Simplic.AdaptiveTesting.Indicator
    {
        #region Private Member
        private Stopwatch stopwatch;
        private long result;
        #endregion

        /// <summary>
        /// Create indicator for duration measuring
        /// </summary>
        /// <param name="settings">Settings block</param>
        public DurationIndicator(IDictionary<string, string> settings)
        {
            result = 0;
        }

        #region Public Methods
        /// <summary>
        /// Return duration as milliseconds
        /// </summary>
        /// <returns>Duration as milliseconds (long)</returns>
        public override object GetResult()
        {
            return result;
        }

        /// <summary>
        /// Prepare durating measuring
        /// </summary>
        public override void Prepare()
        {
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Start duration mearusing
        /// </summary>
        public override void Start()
        {
            stopwatch.Start();
        }

        /// <summary>
        /// Stop duration measuring
        /// </summary>
        public override void Stop()
        {
            stopwatch.Stop();
            result = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Free all resources, which will not be needed any more
        /// </summary>
        public override void Free()
        {
            stopwatch = null; // example
        }


        public override string ToString()
        {
            return "Test case duration: " + result / 1000d + "s";
        }
        #endregion
    }
}
