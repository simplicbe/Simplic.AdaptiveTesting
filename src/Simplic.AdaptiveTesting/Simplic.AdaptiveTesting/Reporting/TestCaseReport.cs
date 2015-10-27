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
        private IList<Indicator> indicators;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new TestCaseReport for reporting the complete result of a testcase
        /// </summary>
        /// <param name="result">Instance of a result class</param>
        /// <param name="indicators">List of indicators</param>
        public TestCaseReport(TestResult result, IList<Indicator> indicators)
        {
            this.indicators = indicators;
            this.result = result;
        }
        #endregion
    }
}
