using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Configuration
{
    /// <summary>
    /// Configuration for indicators in a test case
    /// </summary>
    public class TestCaseIndicator
    {
        /// <summary>
        /// Name of the indicator
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Settings for the current test-case indicator
        /// </summary>
        public IDictionary<string, string> Settings
        {
            get;
            set;
        }
    }
}
