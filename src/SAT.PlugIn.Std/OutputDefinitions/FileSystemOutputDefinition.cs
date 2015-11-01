using Simplic.AdaptiveTesting.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.PlugIn.Std.Output
{
    /// <summary>
    /// Definition/PlugIn for saving the result of a test scenario in a json file
    /// </summary>
    [Simplic.AdaptiveTesting.PlugIns.OutputDefinition("fileSystem", typeof(FileSystemOutputDefinition))]
    public class FileSystemOutputDefinition : ReportOutput
    {
        #region Private Member
        private string reportOutput;
        private string report;
        #endregion

        #region Constructor
        /// <summary>
        /// Create simple output for exporting a report to json
        /// </summary>
        /// <param name="testReport">TestReport instance</param>
        /// <param name="settings">Output configuration</param>
        public FileSystemOutputDefinition(TestReport testReport, IDictionary<string, string> settings)
            : base(testReport)
        {
            if (settings == null)
            {
                throw new Exception("Could not find reportOutput-Settings in fileSystem output configuration");
            }

            if (!settings.ContainsKey("reportOutput"))
            {
                throw new Exception("Could not find reportOutput in fileSystem output settings");
            }

            reportOutput = settings["reportOutput"];
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create json content
        /// </summary>
        public override void Build()
        {
            report = Newtonsoft.Json.JsonConvert.SerializeObject(TestReport.TestCaseReports);
        }

        /// <summary>
        /// Distribute json file
        /// </summary>
        public override void Distribute()
        {
            reportOutput = reportOutput.Replace("[WorkingDir]" , System.Windows.Forms.Application.StartupPath);

            Simplic.IO.DirectoryHelper.CreateDirectoryIfNotExists(reportOutput);

            TestReport.Process.Listener.Write("Output", reportOutput);

            File.WriteAllText(reportOutput, report);
        }
        #endregion
    }
}
