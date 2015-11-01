using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Configuration
{
    public class TestOutputConfiguration
    {
        public string Name
        {
            get;
            set;
        }

        public IDictionary<string, string> Settings
        {
            get;
            set;
        }
    }
}
