using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Testing
{
    /// <summary>
    /// Contains the result of a single test-case. The information this class contains,
    /// will be used for generating test reports
    /// </summary>
    public class TestResult
    {
        #region Private Member
        private string testName;
        private TestCaseExitCode exitCode;
        private string message;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new test-result
        /// </summary>
        /// <param name="testName">Contains the unique name of a test. The uniqueness will not be proofed</param>
        public TestResult(string testName)
        {
            this.TestName = testName;
        }
        #endregion

        #region Public Member
        /// <summary>
        /// Name of the test which produce this result. Will be part of a test report
        /// </summary>
        public string TestName
        {
            get
            {
                return testName;
            }

            set
            {
                testName = value;
            }
        }

        /// <summary>
        /// Exitcode/status of the result. Will be part of a test report
        /// </summary>
        public TestCaseExitCode ExitCode
        {
            get
            {
                return exitCode;
            }

            set
            {
                exitCode = value;
            }
        }

        /// <summary>
        /// Messagetext of the result. Will be part of a test report
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
        #endregion
    }
}
