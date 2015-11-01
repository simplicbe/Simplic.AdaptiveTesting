using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.PlugIns
{
    /// <summary>
    /// Attribute which must be decorated over all indicator definitions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IndicatorDefinitionAttribute : PlugInBaseAttribute
    {
        /// <summary>
        /// Create new plugin definition for indicators
        /// </summary>
        /// <param name="name">Name of the plugin</param>
        /// <param name="type">Type of the plugin</param>
        public IndicatorDefinitionAttribute(string name, Type type) : base(name)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            Type = type;
        }

        /// <summary>
        /// Type of the testcase definition
        /// </summary>
        public Type Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Override equals for comparison
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is IndicatorDefinitionAttribute)
            {
                return Name == ((IndicatorDefinitionAttribute)obj).Name;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get attribute hashcode
        /// </summary>
        /// <returns>Integer as hash-coed</returns>
        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }
    }
}
