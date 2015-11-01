using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Reporting
{
    /// <summary>
    /// Template for generating reports.
    /// </summary>
    public abstract class ReportOutput
    {
        #region Private Member
        private TestReport testReport;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new report output base
        /// </summary>
        /// <param name="testReport">instance of the reporting system</param>
        public ReportOutput(TestReport testReport)
        {
            this.testReport = testReport;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build the current report
        /// </summary>
        public abstract void Build();

        /// <summary>
        /// Distribute the current report
        /// </summary>
        public abstract void Distribute();
        #endregion

        #region Public Member
        /// <summary>
        /// Instance of the current test-report
        /// </summary>
        public TestReport TestReport
        {
            get
            {
                return testReport;
            }
        }

        /// <summary>
        /// Name of the report output
        /// </summary>
        public string Name
        {
            get;
            internal set;
        }
        #endregion
    }
}
