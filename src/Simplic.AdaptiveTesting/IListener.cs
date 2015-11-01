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
    public interface IListener
    {
        /// <summary>
        /// Write message to some error output
        /// </summary>
        /// <param name="area">Area where the message will be promted from</param>
        /// <param name="message">Detailed message</param>
        void Write(string area, string message);

        /// <summary>
        /// Write error to some error output
        /// </summary>
        /// <param name="area">Area where the message will be promted from</param>
        /// <param name="message">Detailed message</param>
        void Error(string area, string message);

        /// <summary>
        /// Write warning to some error output
        /// </summary>
        /// <param name="area">Area where the message will be promted from</param>
        /// <param name="message">Detailed message</param>
        void Warning(string area, string message);

        /// <summary>
        /// Write success to some error output
        /// </summary>
        /// <param name="area">Area where the message will be promted from</param>
        /// <param name="message">Detailed message</param>
        void Success(string area, string message);
    }
}
