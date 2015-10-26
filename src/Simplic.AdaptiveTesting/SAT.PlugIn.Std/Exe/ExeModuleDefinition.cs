using Simplic.AdaptiveTesting.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.PlugIn.Std
{
    [Simplic.AdaptiveTesting.PlugIns.PlugInDefinition("exe", typeof(ExeModuleDefinition))]
    public class ExeModuleDefinition : TestCase
    {
        public ExeModuleDefinition(string testName, IDictionary<string, string> options) : base(testName, options)
        {
        }

        public override TestResult Process()
        {
            return null;
        }
    }
}
