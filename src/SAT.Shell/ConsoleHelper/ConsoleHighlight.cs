using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Shell
{
    /// <summary>
    /// Create control green highlighting
    /// </summary>
    internal class ConsoleSuccess : ConsoleHighlight
    {
        /// <summary>
        /// Passed green to ConsoleHighlight
        /// </summary>
        public ConsoleSuccess() : base(ConsoleColor.Green)
        {

        }
    }

    /// <summary>
    /// Create control warning highlighting
    /// </summary>
    internal class ConsoleWarning : ConsoleHighlight
    {
        /// <summary>
        /// Passed yellow to ConsoleHighlight
        /// </summary>
        public ConsoleWarning() : base(ConsoleColor.Yellow)
        {

        }
    }

    /// <summary>
    /// Create control error highlighting
    /// </summary>
    internal class ConsoleError : ConsoleHighlight
    {
        /// <summary>
        /// Passed yellow to ConsoleHighlight
        /// </summary>
        public ConsoleError() : base(ConsoleColor.Red)
        {

        }
    }

    /// <summary>
    /// Class for highlighting text in the console output
    /// </summary>
    internal class ConsoleHighlight : IDisposable
    {
        private ConsoleColor resetColor;
        
        /// <summary>
        /// Create highlight object for using in a using-statement
        /// </summary>
        /// <param name="color">New color</param>
        /// <param name="resetColor">Color for reset on disposing</param>
        public ConsoleHighlight(ConsoleColor color, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            this.resetColor = resetColor;
        }

        /// <summary>
        /// Reset color
        /// </summary>
        public void Dispose()
        {
            Console.ForegroundColor = resetColor;
        }
    }
}
