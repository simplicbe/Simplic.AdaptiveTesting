using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// Baseic indicator result
    /// </summary>
    public interface IIndicatorResult
    {
        /// <summary>
        /// Exit code, default is Success
        /// </summary>
        Testing.TestCaseExitCode ExitCode
        {
            get;
            set;
        }

        /// <summary>
        /// Message for adding to report
        /// </summary>
        string Message
        {
            get;
            set;
        }
    }
}
