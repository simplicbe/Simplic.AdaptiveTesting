using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.Testing
{
    /// <summary>
    /// Class to store a collection of testcases. This class directly implement IEnumeratble to be an iterable list of test cases
    /// </summary>
    public class TestCollection : IEnumerable<TestCase>
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
        /// Add a simple testCase to the list of test-cases
        /// </summary>
        /// <param name="testCase">Instance of a test case</param>
        public void Add(TestCase testCase)
        {
            if (testCase == null)
            {
                throw new ArgumentNullException("testCase");
            }

            testCases.Add(testCase);
        }

        /// <summary>
        /// Get enumrator for iterating through the test cases
        /// </summary>
        /// <returns>Yield test cases</returns>
        public IEnumerator<TestCase> GetEnumerator()
        {
            foreach (var testCase in testCases)
            {
                yield return testCase;       
            }
        }

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
