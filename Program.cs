
/*
    Mouse Jiggler
    Copyright © 2018 Thomas George

    This file is part of Mouse Jiggler.

    Mouse Jiggler is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Mouse Jiggler is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Mouse Jiggler.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseJiggler
{
    static class Program
    {
        internal static bool JiggleEnabled = false;

        internal static bool GhostJiggle = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex(false, "MouseJiggler");
            if (mutex.WaitOne(0, false))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string str = args[i];

                    if (args[i].ToLower() == "-jiggle" || args[i].ToLower() == "-j")
                        JiggleEnabled = true;
                    else if (args[i].ToLower() == "-ghost" || args[i].ToLower() == "-g")
                        GhostJiggle = true;
                    else
                    {
                        Console.WriteLine("Invalid Command Line Switch!");
                        return;
                    }
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MouseJigglerApplicationContext());
            }
        }
    }
}
