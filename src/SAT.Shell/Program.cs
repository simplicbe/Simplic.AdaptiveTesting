﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Shell
{
    class Program
    {
        /// <summary>
        /// Main shell for adpative testing
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("- " + new string('-', Console.BufferWidth - 3));
            Console.WriteLine("- Simplic.AdaptiveTesting © EDV-Systeme Spiegelburg GmbH 2015");
            Console.WriteLine("- Version: 0.1.0.0A");
            Console.WriteLine("- More information under: https://github.com/simplicbe/Simplic.AdaptiveTesting/");
            Console.WriteLine("- " + new string('-', Console.BufferWidth - 3));

            // no command
            if (args.Length == 0)
            {
                using (var _waring = new ConsoleWarning())
                {
                    Console.WriteLine("No arguments passed.");
                }

                return;
            }

            string plugInPath = System.Windows.Forms.Application.StartupPath + "\\PlugIns\\";
            
            // Load PlugIn-Dlls
            if (System.IO.Directory.Exists(plugInPath))
            {
                foreach (var plugIn in System.IO.Directory.GetFiles(plugInPath))
                {
                    var plugInName = System.IO.Path.GetFileName(plugIn);

                    if (plugInName.StartsWith("SAT.PlugIn") && plugInName.EndsWith(".dll"))
                    {
                        try
                        {
                            AppDomain.CurrentDomain.Load(System.IO.File.ReadAllBytes(plugIn));
                            Console.WriteLine("PlugIn loaded: " + System.IO.Path.GetFileName(plugIn));
                        }
                        catch (Exception ex)
                        {
                            using (ConsoleError _error = new ConsoleError())
                            {
                                Console.WriteLine("Could not load plugin: `" + plugIn + "`: " + ex.Message);
                            }
                        }
                    }
                }
            }

            // Create new simplic command shell context for processing the input arguments
            var context = Simplic.CommandShell.CommandShellManager.Singleton.CreateShellContext("sat");

            // Register all commands
            context.RegisterMethod("test", TestingCommands.Test, "path");

            // Execute command
            bool errorOccured = false;
            string cmd = "";

            foreach (string part in args)
            {
                if (cmd != "")
                {
                    cmd += " ";
                }

                cmd += part;
            }

            string result = context.Execute(cmd, out errorOccured);

            if (errorOccured)
            {
                using (var _waring = new ConsoleError())
                {
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}
