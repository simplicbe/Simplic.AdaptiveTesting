using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    /// <summary>
    /// Interface to get error during testing/loading
    /// </summary>
    public interface IErrorListener
    {
        /// <summary>
        /// Write error to some error output
        /// </summary>
        /// <param name="area">Area where the error occures</param>
        /// <param name="message">Detailed error message</param>
        void Write(string area, string message);
    }
}
