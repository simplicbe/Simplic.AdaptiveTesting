using Newtonsoft.Json;
using Simplic.AdaptiveTesting.Configuration;
using Simplic.AdaptiveTesting.Reporting;
using Simplic.AdaptiveTesting.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// The test-process is the main class for executing a complete test containins:
    /// 1. Configuration loading
    /// 2. Test case execution
    /// 3. Report generation
    /// </summary>
    public class TestProcess
    {
        #region Private Member
        private TestCollection testCollection;
        private TestReport testReport;
        private TestConfiguration configuration;
        private IListener listener;
        private bool errorsOccured;

        private IDictionary<string, Type> testModules;
        #endregion

        #region Constructor
        /// <summary>
        /// Create test-process by a jsonConfiguration
        /// </summary>
        /// <param name="jsonConfiguration">Configuration of the current adaptive test.</param>
        /// <param name="listener">Error-Listener</param>
        /// <param name="autoPlugInDetection">If set to true, the system will look for plugins in the current AppDomain</param>
        public TestProcess(string jsonConfiguration, IListener listener, bool autoPlugInDetection = true)
        {
            testCollection = new TestCollection();
            testReport = new TestReport();

            testModules = new Dictionary<string, Type>();

            this.listener = listener;

            configuration = JsonConvert.DeserializeObject<TestConfiguration>(jsonConfiguration);

            // Validate configuration
            errorsOccured = !Validate(configuration);

            // Load module definitions if autoPlugInDetection is enabled
            if (autoPlugInDetection)
            {
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (asm.FullName.Contains("SAT.PlugIn"))
                    {
                        foreach (Type cType in asm.GetTypes())
                        {
                            // Find Attributes
                            Attribute[] cAttributes = System.Attribute.GetCustomAttributes(cType);

                            if (cAttributes != null)
                            {
                                // Iterate throught all attributes
                                foreach (PlugIns.ModuleDefinitionAttribute def in cAttributes)
                                {
                                    if (def is PlugIns.ModuleDefinitionAttribute)
                                    {
                                        listener.Write("ModuleDef", def.Name);
                                        AddTestModule(def.Name, def.Type);
                                    }
                                }
                            }
                        }
                    }
                }
            }
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
                listener.Error("Configuration.Validation", "Could not find `settings` in Testproject configuration");
                returnValue = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(configuration.Settings.Project))
                {
                    listener.Error("Configuration.Validation", "Could not find `project`-name in `settings`");
                    returnValue = false;
                }

                if (string.IsNullOrWhiteSpace(configuration.Settings.ReportOutput))
                {
                    listener.Error("Configuration.Validation", "Could not find `reportOutput` in `settings`");
                    returnValue = false;
                }
                else
                {
                    try
                    {
                        if (!configuration.Settings.ReportOutput.StartsWith("\\"))
                        {
                            configuration.Settings.ReportOutput = "\\" + configuration.Settings.ReportOutput;
                        }
                        if (!configuration.Settings.ReportOutput.EndsWith("\\"))
                        {
                            configuration.Settings.ReportOutput += "\\";
                        }

                        // create complete output path
                        configuration.Settings.ReportOutput = System.Windows.Forms.Application.StartupPath + configuration.Settings.ReportOutput;

                        IO.DirectoryHelper.CreateDirectoryIfNotExists(configuration.Settings.ReportOutput);
                    }
                    catch (Exception ex)
                    {
                        listener.Error("Configuration.CreateRptDirectory", "Could not create report output directory: " + configuration.Settings.ReportOutput + " - " + ex.Message);
                    }
                }
            }

            if (configuration.TestCases == null || configuration.TestCases.Count == 0)
            {
                listener.Error("Configuration.Validation", "Could not find any `testCase` in Testproject configuration");
                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #endregion

        #region Public Methods

        #region [Start]
        /// <summary>
        /// Start the test process
        /// </summary>
        public void Start()
        {
            if (configuration == null || errorsOccured)
            {
                listener.Error("Process.NoConfiguration", "No configuration loaded");
                return;
            }
            else
            {
                Console.WriteLine();
                listener.Success("Process", "Start: " + configuration.Settings.Project);

                // Create all needed test-cases
                listener.Write("Procee", "Create test-cases");

                foreach (var caseConfig in configuration.TestCases)
                {
                    if (testModules.ContainsKey(caseConfig.Type))
                    {
                        // Create instance of a test-case
                        var tc = (TestCase)Activator.CreateInstance(
                                        testModules[caseConfig.Type],
                                        caseConfig.Name,
                                        caseConfig.Options
                                    );

                        // Add to collection
                        testCollection.Add(tc);
                    }
                    else
                    {
                        listener.Error("Process.TestCase", "Could not find module definition for test-case: `" + (caseConfig.Type ?? "") + "`");
                    }
                }

                // Execute tests
                foreach (TestCase testCase in testCollection)
                {
                    listener.Write("Start", testCase.TestName);

                    var output = testCase.Execute();

                    // Output
                    if (output != null)
                    {
                        if (output.ExitCode == TestCaseExitCode.Success)
                        {
                            listener.Success("Success", output.Message);
                        }
                        else if (output.ExitCode == TestCaseExitCode.Warning)
                        {
                            listener.Warning("Warning", output.Message);
                        }
                        else if (output.ExitCode == TestCaseExitCode.Error)
                        {
                            listener.Error("Error", output.Message);
                        }
                    }

                    // Return indicator values
                    foreach (var indicator in testCase.Indicators)
                    {
                        listener.Write("Indicator", indicator.ToString());
                    }

                    // Create report-entry
                    var rptCase = new TestCaseReport(output, testCase.Indicators);
                    testReport.TestCaseReports.Add(rptCase);
                }
            }
        }
        #endregion

        #region [AddTestModule]
        /// <summary>
        /// Add a module definition to the current TestProcess
        /// </summary>
        /// <param name="name">Unique name of the module definition</param>
        /// <param name="type">Type for createing a test-module</param>
        public void AddTestModule(string name, Type type)
        {
            if (!testModules.ContainsKey(name))
            {
                testModules.Add(name, type);
            }
            else
            {
                listener.Error("TestMoulde.Add", "The module definition: " + name + " is already existing");
                errorsOccured = true;
            }
        }
        #endregion

        #endregion

        #region Public Member

        #endregion
    }
}
