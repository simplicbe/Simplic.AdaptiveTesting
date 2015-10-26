using Simplic.AdaptiveTesting;
using Simplic.CommandShell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Shell
{
    /// <summary>
    /// Class which contains all commands for testing purpose
    /// </summary>
    public static class TestingCommands
    {
        #region [Test]
        [ParameterDescription("path", true, "Path to the scripts, containg all test information and configurtion")]
        [CommandDescription("Execute a test-script and create a report")]
        public static string Test(string command, CommandShellParameterCollection parameter)
        {
            string path = parameter.GetParameterValueAsString("path");

            if (File.Exists(path))
            {
                Console.WriteLine("Execute test-file: {0}", path);
                var process = new Simplic.AdaptiveTesting.TestProcess(File.ReadAllText(path), new ErrorListener());
            }
            else
            {
                using (var _error = new ConsoleError())
                {
                    Console.WriteLine("Could not find configuration '{0}'", path);
                }
            }

            return "";
        }
        #endregion
    }

    /// <summary>
    /// Error-Listener for error output
    /// </summary>
    public class ErrorListener : IErrorListener
    {
        /// <summary>
        /// Print error
        /// </summary>
        /// <param name="area">Area where the error occured</param>
        /// <param name="message">Detail message text</param>
        public void Write(string area, string message)
        {
            using (ConsoleError _error = new ConsoleError())
            {
                Console.WriteLine((area ?? "NULL") + " -> " + (message ?? "NULL"));
            }
        }
    }
}
