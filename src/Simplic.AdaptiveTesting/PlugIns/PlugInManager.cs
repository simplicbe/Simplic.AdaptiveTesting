using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.PlugIns
{
    /// <summary>
    /// PlugIn manager for handling definitions, e.g. inidicator, test cases, ...
    /// </summary>
    public class PlugInManager
    {
        #region Pirvate Member
        private IList<PlugInBaseAttribute> plugIns;
        private IListener listener;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new plugIn manager instance
        /// </summary>
        /// <param name="listener">Listener for output</param>
        public PlugInManager(IListener listener)
        {
            plugIns = new List<PlugInBaseAttribute>();
            this.listener = listener;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load all available PlugIns and refresh list of plugIns
        /// </summary>
        public void LoadPlugIns()
        {
            plugIns = new List<PlugInBaseAttribute>();

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.FullName.Contains("SAT.PlugIn"))
                {
                    foreach (Type cType in asm.GetTypes())
                    {
                        // Find Attributes
                        Attribute[] cAttributes = System.Attribute.GetCustomAttributes(cType);

                        if (cAttributes != null)
                        {
                            // Iterate throught all attributes
                            foreach (PlugInBaseAttribute def in cAttributes)
                            {
                                if (def is PlugInBaseAttribute)
                                {
                                    listener.Write("PlugIn", def.Name);
                                    plugIns.Add(def);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get definition of a plugin of a specific type by name
        /// </summary>
        /// <typeparam name="T">Type of PlugInBase</typeparam>
        /// <param name="name">Name of the plugin</param>
        /// <returns>If found an instance of a plugin definition</returns>
        public T GetPlugInDefinition<T>(string name) where T : PlugInBaseAttribute
        {
            var _plugIns = plugIns.Where(item => item.Name == name).OfType<T>().ToList();

            if (_plugIns.Count > 1)
            {
                listener.Warning("PlugIns", "PlugIn: `" + name + "` exists more than one time");
            }

            return _plugIns.FirstOrDefault();
        }
        #endregion
    }
}
