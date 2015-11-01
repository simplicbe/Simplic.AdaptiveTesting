using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Testing
{
    /// <summary>
    /// Every TestCase returns one of the following enum values as exit code
    /// </summary>
    public enum TestCaseExitCode
    {
        /// <summary>
        /// Test tests completes successful
        /// </summary>
        Success = 0,

        /// <summary>
        /// The test completes with warnings
        /// </summary>
        Warning = 1,

        /// <summary>
        /// The test completes/finishs with errors.
        /// </summary>
        Error = 2
    }
}
