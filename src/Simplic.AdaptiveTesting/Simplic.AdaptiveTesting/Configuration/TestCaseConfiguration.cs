using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Configuration
{
    /// <summary>
    /// Contains the configuration of a single test case
    /// </summary>
    public class TestCaseConfiguration
    {
        /// <summary>
        /// Name of the test-case
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Type of the test-case
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Options for the current test-case
        /// </summary>
        public IDictionary<string, string> Options
        {
            get;
            set;
        }
    }
}
