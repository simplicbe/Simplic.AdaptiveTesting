using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.PlugIns
{
    /// <summary>
    /// Attribute type which must be implemented in all attributes which 
    /// will be used for extending the framework
    /// </summary>
    public abstract class PlugInBaseAttribute : Attribute
    {
        private string name;

        /// <summary>
        /// Craete new plugin base
        /// </summary>
        /// <param name="name">Name of the plugin</param>
        public PlugInBaseAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException("name", "Name could not be null or whitespace");
            }

            this.name = name;
        }

        /// <summary>
        /// Name of the extension/plugin
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
