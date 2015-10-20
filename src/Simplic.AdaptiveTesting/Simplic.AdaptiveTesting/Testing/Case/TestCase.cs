using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Testing
{
    /// <summary>
    /// A testcase represents the base for all test-cases and must be inherited for all specific tests.
    /// Test default testcase has all base members and functionality every test should have
    /// </summary>
    public abstract class TestCase
    {
        #region Private Member
        private string testName;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new test-case
        /// </summary>
        /// <param name="testName">Unique name of the testcase. uniqueness will not be proofed</param>
        public TestCase(string testName)
        {
            this.TestName = testName;
        }
        #endregion

        #region Public Member
        /// <summary>
        /// Name of the test which produce this result. Will be part of a test report
        /// </summary>
        public string TestName
        {
            get
            {
                return testName;
            }

            set
            {
                testName = value;
            }
        }
        #endregion
    }
}
