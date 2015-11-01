using Simplic.AdaptiveTesting.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Reporting
{
    /// <summary>
    /// Contains the report for a single test-case
    /// </summary>
    public class TestCaseReport
    {
        #region Private Member
        private TestResult result;
        private IList<IIndicatorResult> indicatorResults;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new TestCaseReport for reporting the complete result of a testcase
        /// </summary>
        /// <param name="result">Instance of a result class</param>
        /// <param name="indicatorResults">List of indicator results</param>
        public TestCaseReport(TestResult result, IList<IIndicatorResult> indicatorResults)
        {
            this.indicatorResults = indicatorResults;
            this.result = result;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Raw result of the test
        /// </summary>
        public TestResult Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// Indicators for the current test
        /// </summary>
        public IList<IIndicatorResult> IndicatorResults
        {
            get
            {
                return indicatorResults;
            }
        }
        #endregion
    }
}
