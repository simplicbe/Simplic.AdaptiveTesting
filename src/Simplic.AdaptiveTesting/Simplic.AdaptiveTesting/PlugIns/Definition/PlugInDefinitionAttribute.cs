using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.PlugIns
{
    /// <summary>
    /// Attribute which must be decorated over all module definitions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PlugInDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Contains all loaded PlugIn definitions
        /// </summary>
        public static IList<PlugInDefinitionAttribute> LoadedDefinitions
        {
            get;
            private set;
        }

        /// <summary>
        /// Create new plugindefinition
        /// </summary>
        /// <param name="name">Name of the plugin</param>
        /// <param name="type">Type of the plugin</param>
        public PlugInDefinitionAttribute(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException("name", "Name could not be null or whitespace");
            }

            if(type == null)
            {
                throw new ArgumentNullException("type");
            }

            Name = name;
            Type = type;

            if (LoadedDefinitions == null)
            {
                LoadedDefinitions = new List<PlugInDefinitionAttribute>();
            }

            // Add to definition list
            if(!LoadedDefinitions.Contains(this))
            {
                LoadedDefinitions.Add(this);
            }
        }

        /// <summary>
        /// Unique name of the module definition / testcase definition
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Type of the module definition / testcase definition
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
            else if (obj is PlugInDefinitionAttribute)
            {
                return Name == ((PlugInDefinitionAttribute)obj).Name;
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
