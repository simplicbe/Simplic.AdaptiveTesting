using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Reporting
{
    /// <summary>
    /// Represents the complete test-report over all TestCaseReports
    /// </summary>
    public class TestReport
    {
        #region Private Member
        private IList<TestCaseReport> testCaseReports;
        #endregion

        #region Constructor
        /// <summary>
        /// Create TestReporing
        /// </summary>
        public TestReport()
        {
            testCaseReports = new List<TestCaseReport>();
        }
        #endregion

        #region Public Member
        /// <summary>
        /// List of all reports for a test scenario
        /// </summary>
        public IList<TestCaseReport> TestCaseReports
        {
            get
            {
                return testCaseReports;
            }
        }
        #endregion
    }
}
