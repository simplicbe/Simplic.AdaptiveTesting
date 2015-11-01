using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Testing
{
    /// <summary>
    /// A testcase represents the base for all test-cases and must be inherited for all specific tests.
    /// Test default testcase has all base members and functionality every test should have
    /// </summary>
    public abstract class TestCase
    {
        #region Private Member
        private string testName;
        private IList<Indicator> indicators;
        private IDictionary<string, string> options;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new test-case
        /// </summary>
        /// <param name="testName">Unique name of the testcase. uniqueness will not be proofed</param>
        /// <param name="options">Options for the test configuration</param>
        public TestCase(string testName, IDictionary<string, string> options)
        {
            this.TestName = testName;
            indicators = new List<Indicator>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Execute the current test
        /// </summary>
        /// <returns>Test-Result instance containing all result information</returns>
        public virtual TestResult Execute()
        {
            TestResult testResult = null;

            try
            {
                // Prepare all indicators
                foreach (var indicator in indicators)
                {
                    indicator.Prepare();
                }

                // Before test starts
                foreach (var indicator in indicators)
                {
                    indicator.Start();
                }

                // Execute test
                testResult = Process();

                // Test finished, stop indicator
                foreach (var indicator in indicators)
                {
                    indicator.Stop();
                }

                // free indicator resources
                foreach (var indicator in indicators)
                {
                    indicator.Free();
                }
            }
            catch(Exception ex)
            {
                // Create TestResult and add exception information
                testResult = new TestResult(this);
                testResult.ExitCode = TestCaseExitCode.Error;
                testResult.Message = ex.ToString() + (ex.InnerException == null ? "" : "\r\n" + ex.InnerException.ToString());
            }

            return testResult;
        }

        /// <summary>
        /// Must be implemented in all test-case implementations. In this class the test must be executed
        /// </summary>
        /// <returns>Instance of a test-result for report generating</returns>
        protected abstract TestResult Process();

        /// <summary>
        /// Add an indicator to the list of indicators for the current test-case
        /// </summary>
        /// <param name="indicator">Instance of an indicator</param>
        public virtual void AddIndicator(Indicator indicator)
        {
            if (indicator == null)
            {
                throw new ArgumentNullException("indicator");
            }

            indicators.Add(indicator);
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
        /// Get a list of available indicators
        /// </summary>
        public IList<Indicator> Indicators
        {
            get
            {
                return indicators;
            }
        }

        /// <summary>
        /// Test-case options from configuration
        /// </summary>
        public IDictionary<string, string> Options
        {
            get
            {
                return options;
            }
        }
        #endregion
    }
}
