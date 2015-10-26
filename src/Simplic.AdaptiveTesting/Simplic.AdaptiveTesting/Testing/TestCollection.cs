using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Testing
{
    /// <summary>
    /// Contains a set of testcases
    /// </summary>
    public class TestCollection
    {
        private IList<TestCase> testCases;

        /// <summary>
        /// Create new test-case collection
        /// </summary>
        public TestCollection()
        {
            testCases = new List<TestCase>();
        }

        /// <summary>
        /// List of test.cases
        /// </summary>
        public IList<TestCase> TestCases
        {
            get
            {
                return testCases;
            }

            set
            {
                testCases = value;
            }
        }
    }
}
