using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Configuration
{
    /// <summary>
    /// Contains a complete test configuration
    /// </summary>
    public class TestConfiguration
    {
        /// <summary>
        /// Settings for the current project
        /// </summary>
        public TestSettingsConfiguration Settings
        {
            get;
            set;
        }

        /// <summary>
        /// Contains all test cases
        /// </summary>
        public IList<TestCaseConfiguration> TestCases
        {
            get;
            set;
        }
    }
}
