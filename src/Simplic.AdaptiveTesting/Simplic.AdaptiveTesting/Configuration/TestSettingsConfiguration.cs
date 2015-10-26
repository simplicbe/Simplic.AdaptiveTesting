using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Configuration
{
    /// <summary>
    /// Contains the settings for the current test
    /// </summary>
    public class TestSettingsConfiguration
    {
        /// <summary>
        /// Name of the project
        /// </summary>
        public string Project
        {
            get;
            set;
        }

        /// <summary>
        /// Report-Output path
        /// </summary>
        public string ReportOutput
        {
            get;
            set;
        }
    }
}
