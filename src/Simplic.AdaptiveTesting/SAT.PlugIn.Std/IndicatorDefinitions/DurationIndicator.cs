using Simplic.AdaptiveTesting;
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
    public class DurationIndicator : Indicator
    {
        #region Private Member
        private Stopwatch stopwatch;
        private long max;
        private IndicatorResult<long> result;
        #endregion

        /// <summary>
        /// Create indicator for duration measuring
        /// </summary>
        /// <param name="settings">Settings block</param>
        public DurationIndicator(IDictionary<string, string> settings)
        {
            if (settings.ContainsKey("max"))
            {
                if (!long.TryParse(settings["max"], out max))
                {
                    throw new Exception("Max setting must be a valid long: " + settings["max"] ?? "NULL");
                }
            }
        }

        #region Public Methods
        /// <summary>
        /// Return duration as milliseconds
        /// </summary>
        /// <returns>Indicator result: IndicatorResult<long></returns>
        public override IIndicatorResult GetResult()
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

            result = new IndicatorResult<long>();
            result.Result = stopwatch.ElapsedMilliseconds;

            if (max > 0)
            {
                if (result.Result > max)
                {
                    result.ExitCode = Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Error;
                    result.Message = String.Format("Test-case tooks to long to run. Maximum: {0}s, duration: {1}s",  + max / 1000d, result.Result / 1000d);
                }
                else
                {
                    result.ExitCode = Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Success;
                    result.Message = "Duration indicator runs successful: " + result.Result / 1000d + "s";
                }
            }
            else
            {
                result.ExitCode = Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Success;
                result.Message = "Duration indicator runs successful: " + result.Result / 1000d + "s";
            }
        }

        /// <summary>
        /// Free all resources, which will not be needed any more
        /// </summary>
        public override void Free()
        {
            stopwatch = null; // example
        }

        /// <summary>
        /// Return as string
        /// </summary>
        /// <returns>Indicator as string</returns>
        public override string ToString()
        {
            return result == null ? "No result yet" : result.Message;
        }
        #endregion
    }
}
