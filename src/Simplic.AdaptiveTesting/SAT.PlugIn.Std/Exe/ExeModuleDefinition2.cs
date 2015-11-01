using Simplic.AdaptiveTesting.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.PlugIn.Std
{
    /// <summary>
    /// Test which simply execute a windows executable
    /// </summary>
    [Simplic.AdaptiveTesting.PlugIns.TestCaseDefinition("exe", typeof(ExeModuleDefinition))]
    public class ExeModuleDefinition2 : TestCase
    {
        private string executable;
        private string arguments;
        private string workingDirectory;

        /// <summary>
        /// Create new exe module
        /// </summary>
        /// <param name="testName">Name of the test</param>
        /// <param name="options">Options. Must contains a path variable</param>
        public ExeModuleDefinition2(string testName, IDictionary<string, string> options) : base(testName, options)
        {
            if (!options.ContainsKey("path"))
            {
                throw new Exception(TestName + " has to contains a path to an executable");
            }
            else
            {
                executable = options["path"];
            }

            if (options.ContainsKey("args"))
            {
                arguments = options["args"];
            }

            if (options.ContainsKey("workingDir"))
            {
                workingDirectory = options["workingDir"];
            }
        }

        /// <summary>
        /// Process/execute the test
        /// </summary>
        /// <returns>Result with test results</returns>
        protected override TestResult Process()
        {
            TestResult result = new TestResult(this);

            Process process = new Process();

            process.StartInfo.FileName = executable;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            if (!string.IsNullOrWhiteSpace(arguments))
            {
                process.StartInfo.Arguments = arguments;
            }

            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                process.StartInfo.WorkingDirectory = workingDirectory;
            }

            process.Start();
            process.WaitForExit();

            // set result
            if (process.ExitCode == 0)
            {
                result.ExitCode = TestCaseExitCode.Success;
            }
            else
            {
                result.ExitCode = TestCaseExitCode.Warning;
            }

            result.Message = process.StandardOutput.ReadToEnd();

            return result;
        }
    }
}
