using Newtonsoft.Json;
using Simplic.AdaptiveTesting.Configuration;
using Simplic.AdaptiveTesting.Reporting;
using Simplic.AdaptiveTesting.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// The test-process is the main class for executing a complete test containins:
    /// 1. Test case execution
    /// 2. Report generation
    /// </summary>
    public class TestProcess
    {
        #region Private Member
        private TestCollection testCollection;
        private TestReport testReport;
        private TestConfiguration configuration;
        private IErrorListener errorlistener;
        private bool errorsOccured;
        #endregion

        #region Constructor
        /// <summary>
        /// Create test-process by a jsonConfiguration
        /// </summary>
        /// <param name="jsonConfiguration">Configuration of the current adaptive test.</param>
        /// <param name="listener">Error-Listener</param>
        public TestProcess(string jsonConfiguration, IErrorListener listener)
        {
            errorlistener = listener;

            configuration = JsonConvert.DeserializeObject<TestConfiguration>(jsonConfiguration);
            System.Diagnostics.Debugger.Launch();
            // Validate configuration
            errorsOccured = !Validate(configuration);
        }
        #endregion

        #region Private Methods

        #region [Validate]
        /// <summary>
        /// Validate the configuration model
        /// </summary>
        /// <param name="configuration">Configuration instance</param>
        /// <returns>Instance of a test configuration</returns>
        private bool Validate(TestConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuratio");
            }

            bool returnValue = true;

            if (configuration.Settings == null)
            {
                errorlistener.Write("Configuration.Validation", "Could not find `settings` in Testproject configuration");
                returnValue = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(configuration.Settings.Project))
                {
                    errorlistener.Write("Configuration.Validation", "Could not find `project`-name in `settings`");
                    returnValue = false;
                }
                if (string.IsNullOrWhiteSpace("reportOutput"))
                {
                    errorlistener.Write("Configuration.Validation", "Could not find `reportOutput` in `settings`");
                    returnValue = false;
                }
            }

            if (configuration.TestCases == null || configuration.TestCases.Count == 0)
            {
                errorlistener.Write("Configuration.Validation", "Could not find any `testCase` in Testproject configuration");
                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #endregion

        #region Public Methods

        #endregion

        #region Public Member

        #endregion
    }
}
