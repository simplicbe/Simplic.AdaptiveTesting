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

        // Test statistic
        private int successfullTestCaseCount;
        private int warningTestCaseCount;
        private int errorTestCaseCount;

        private int successfullIndicatorsCount;
        private int warningIndicatorsCount;
        private int errorIndicatorsCount;
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
            // Add testcase statistic
            testCaseReports.ToList().ForEach((TestCaseReport tc) =>
                    {
                        switch (tc.Result.ExitCode)
                        {
                            case Testing.TestCaseExitCode.Success:
                                successfullTestCaseCount++;
                                break;
                            case Testing.TestCaseExitCode.Warning:
                                warningTestCaseCount++;
                                break;
                            case Testing.TestCaseExitCode.Error:
                                errorTestCaseCount++;
                                break;
                        }

                        // Add indicator statistic
                        tc.IndicatorResults.ToList().ForEach((IIndicatorResult ind) =>
                        {
                            switch (ind.ExitCode)
                            {
                                case Testing.TestCaseExitCode.Success:
                                    successfullIndicatorsCount++;
                                    break;
                                case Testing.TestCaseExitCode.Warning:
                                    warningIndicatorsCount++;
                                    break;
                                case Testing.TestCaseExitCode.Error:
                                    errorIndicatorsCount++;
                                    break;
                            }
                        });
                    });

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

        /// <summary>
        /// Return amount/count of test cases
        /// </summary>
        public int TestCasesCount
        {
            get
            {
                return successfullTestCaseCount + warningTestCaseCount + errorTestCaseCount;
            }
        }

        /// <summary>
        /// Amount of successfull test cases
        /// </summary>
        public int SuccessfullTestCaseCount
        {
            get
            {
                return successfullTestCaseCount;
            }
        }

        /// <summary>
        /// Amount of test casis with warnings
        /// </summary>
        public int WarningTestCaseCount
        {
            get
            {
                return warningTestCaseCount;
            }
        }

        /// <summary>
        /// Amount of failed test cases
        /// </summary>
        public int ErrorTestCaseCount
        {
            get
            {
                return errorTestCaseCount;
            }
        }

        /// <summary>
        /// Return the amount of indicators
        /// </summary>
        public int IndicatorsCount
        {
            get
            {
                return successfullIndicatorsCount + warningIndicatorsCount + errorIndicatorsCount;
            }
        }

        /// <summary>
        /// Amount of successfull indicators
        /// </summary>
        public int SuccessfullIndicatorsCount
        {
            get
            {
                return successfullIndicatorsCount;
            }
        }

        /// <summary>
        /// Amount of indicators with warnings
        /// </summary>
        public int WarningIndicatorsCount
        {
            get
            {
                return warningIndicatorsCount;
            }
        }

        /// <summary>
        /// Amount of failed indicators
        /// </summary>
        public int ErrorIndicatorsCount
        {
            get
            {
                return errorIndicatorsCount;
            }
        }
        #endregion
    }
}
