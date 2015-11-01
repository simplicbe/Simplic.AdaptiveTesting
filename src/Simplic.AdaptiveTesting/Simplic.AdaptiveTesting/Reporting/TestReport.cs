using Simplic.AdaptiveTesting.PlugIns;
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
        private IList<Configuration.TestOutputConfiguration> outputConfiguration;
        private IList<ReportOutput> reportOutptus;
        private TestProcess process;
        #endregion

        #region Constructor
        /// <summary>
        /// Create TestReporing
        /// </summary>
        /// <param name="outputConfiguration">Output configuration</param>
        public TestReport(TestProcess process, IList<Configuration.TestOutputConfiguration> outputConfiguration)
        {
            testCaseReports = new List<TestCaseReport>();
            this.outputConfiguration = outputConfiguration;
            this.process = process;

            // Try to get all report outputs
            reportOutptus = new List<ReportOutput>();

            foreach (var config in outputConfiguration)
            {
                var definition = process.PlugInManager.GetPlugInDefinition<OutputDefinitionAttribute>(config.Name);

                if (definition != null)
                {
                    // Create instance of render outptu
                    var ro = (ReportOutput)Activator.CreateInstance(
                            definition.Type,
                            this,
                            config.Settings
                        );

                    ro.Name = config.Name;

                    reportOutptus.Add(ro);
                }
                else
                {
                    process.Listener.Error("ReportOutput", "Could not find output-definition: " + config.Name ?? "NULL");
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build report
        /// </summary>
        public void Build()
        {
            foreach (var output in reportOutptus)
            {
                process.Listener.Write(" ", output.Name);
                output.Build();
            }
        }

        /// <summary>
        /// Distribute the report
        /// </summary>
        public void Distribute()
        {
            foreach (var output in reportOutptus)
            {
                process.Listener.Write(" ", output.Name);
                output.Distribute();
            }
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

        /// <summary>
        /// Process for which the report will be created
        /// </summary>
        public TestProcess Process
        {
            get
            {
                return process;
            }
        }
        #endregion
    }
}
