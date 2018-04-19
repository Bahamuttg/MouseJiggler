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
