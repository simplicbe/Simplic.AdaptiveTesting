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
                var process = new Simplic.AdaptiveTesting.TestProcess(File.ReadAllText(path), new MessageListener());
                process.Start();
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
    /// Listener for message output
    /// </summary>
    public class MessageListener : IListener
    {
        /// <summary>
        /// Print error
        /// </summary>
        /// <param name="area">Area where the error occured</param>
        /// <param name="message">Detail message text</param>
        public void Error(string area, string message)
        {
            using (ConsoleError _error = new ConsoleError())
            {
                Write(area, message);
            }
        }

        /// <summary>
        /// Write success message
        /// </summary>
        /// <param name="area"></param>
        /// <param name="message"></param>
        public void Success(string area, string message)
        {
            using (ConsoleSuccess _success = new ConsoleSuccess())
            {
                Write(area, message);
            }
        }

        /// <summary>
        /// Write warning
        /// </summary>
        /// <param name="area"></param>
        /// <param name="message"></param>
        public void Warning(string area, string message)
        {
            using (ConsoleWarning _success = new ConsoleWarning())
            {
                Write(area, message);
            }
        }

        /// <summary>
        /// Write simple message
        /// </summary>
        /// <param name="area"></param>
        /// <param name="message"></param>
        public void Write(string area, string message)
        {
            Console.WriteLine((area ?? "NULL") + "-> " + (message ?? "NULL"));
        }
    }
}
