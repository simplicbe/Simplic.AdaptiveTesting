using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting.PlugIns
{
    /// <summary>
    /// Attribute which  must be decorate all indicator definitions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IndicatorDefinitionAttribute : Attribute
    {

    }
}
