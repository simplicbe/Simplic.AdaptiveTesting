using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// Result which will be returned by an indicator
    /// </summary>
    public class IndicatorResult<T> : IIndicatorResult
    {
        /// <summary>
        /// Create indicator result
        /// </summary>
        public IndicatorResult()
        {
            ExitCode = Testing.TestCaseExitCode.Success;
        }

        /// <summary>
        /// Exit code, default is Success
        /// </summary>
        public Testing.TestCaseExitCode ExitCode
        {
            get;
            set;
        }

        /// <summary>
        /// Message for adding to report
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Raw result of the indicator
        /// </summary>
        public T Result
        {
            get;
            set;
        }
    }
}
