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
    /// Definition/PlugIn for saving the result of a test scenario in a html file
    /// </summary>
    [Simplic.AdaptiveTesting.PlugIns.OutputDefinition("html", typeof(HtmlOutputDefinition))]
    public class HtmlOutputDefinition : ReportOutput
    {
        #region Private Member
        private string reportOutput;
        private string theme;
        private string report;
        #endregion

        #region Constructor
        /// <summary>
        /// Create simple output for exporting a report to html
        /// </summary>
        /// <param name="testReport">TestReport instance</param>
        /// <param name="settings">Output configuration</param>
        public HtmlOutputDefinition(TestReport testReport, IDictionary<string, string> settings)
            : base(testReport)
        {
            if (settings == null)
            {
                throw new Exception("Could not find reportOutput-Settings in html output configuration");
            }

            if (!settings.ContainsKey("reportOutput"))
            {
                throw new Exception("Could not find reportOutput in html output settings");
            }

            if (!settings.ContainsKey("theme"))
            {
                throw new Exception("Could not find theme in html output settings");
            }

            reportOutput = settings["reportOutput"];
            reportOutput = reportOutput.Replace("[WorkingDir]", System.Windows.Forms.Application.StartupPath);

            theme = settings["theme"];
            theme = theme.Replace("[WorkingDir]", System.Windows.Forms.Application.StartupPath);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create html content
        /// </summary>
        public override void Build()
        {
            if (!File.Exists(theme))
            {
                TestReport.Process.Listener.Error("HtmlReport", "Could not find theme: " + theme);
                report = "";
            }
            else
            {
                report = File.ReadAllText(theme);
            }

            StringBuilder sb = new StringBuilder();

            // Report test-case result
            foreach (var res in TestReport.TestCaseReports)
            {
                sb.AppendLine(TestCaseHeader(res.Result.TestName));

                if (res.Result.ExitCode == Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Success)
                {
                    sb.AppendLine( Success(res.Result.Message));
                }
                else if (res.Result.ExitCode == Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Warning)
                {
                    sb.AppendLine(Warning(res.Result.Message));
                }
                else if (res.Result.ExitCode == Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Error)
                {
                    sb.AppendLine(Error(res.Result.Message));
                }

                // Indicator output
                if (res.IndicatorResults.Count > 0)
                {
                    sb.AppendLine("Indicator");

                    foreach (var indicator in res.IndicatorResults)
                    {
                        if (indicator.ExitCode == Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Success)
                        {
                            sb.AppendLine(Success(indicator.Message));
                        }
                        else if (indicator.ExitCode == Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Warning)
                        {
                            sb.AppendLine(Warning(indicator.Message));
                        }
                        else if (indicator.ExitCode == Simplic.AdaptiveTesting.Testing.TestCaseExitCode.Error)
                        {
                            sb.AppendLine(Error(indicator.Message));
                        }
                    }
                }

                sb.AppendLine("<hr />");
            }

            report = report.Replace("{content}", sb.ToString());
        }

        private string Info(string content)
        {
            return String.Format("<div class=\"info\">{0}</div>", content.Replace(Environment.NewLine, "<br />"));
        }

        private string Warning(string content)
        {
            return String.Format("<div class=\"warning\">{0}</div>", content.Replace(Environment.NewLine, "<br />"));
        }

        private string Success(string content)
        {
            return String.Format("<div class=\"success\">{0}</div>", content.Replace(Environment.NewLine, "<br />"));
        }

        private string Error(string content)
        {
            return String.Format("<div class=\"error\">{0}</div>", content.Replace(Environment.NewLine, "<br />"));
        }

        private string TestCaseHeader(string name)
        {
            return String.Format("<h1>{0}</h1>", name);
        }

        private string IndicatorHeader(string name)
        {
            return String.Format("<h3>{0}</h3>", name);
        }

        /// <summary>
        /// Distribute json file
        /// </summary>
        public override void Distribute()
        {
            Simplic.IO.DirectoryHelper.CreateDirectoryIfNotExists(reportOutput);

            TestReport.Process.Listener.Write("Output", reportOutput);

            File.WriteAllText(reportOutput, report);
        }
        #endregion
    }
}
