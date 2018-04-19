using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MouseJiggler
{
    internal struct INPUT
    {
        public int TYPE;

        public int dx;

        public int dy;

        public int mouseData;

        public int dwFlags;

        public int time;

        public IntPtr dwExtraInfo;
    }
    /// <summary>
    /// Jiggles the mouse through the WIN32 interface
    /// </summary>
    public static class Jiggler
    {
        public static void Jiggle(int dx, int dy)
        {
            INPUT nPUT = new INPUT()
            {
                TYPE = 0,
                dx = dx,
                dy = dy,
                mouseData = 0,
                dwFlags = 1,
                time = 0,
                dwExtraInfo = (IntPtr)0
            };
            if (Jiggler.SendInput(1, ref nPUT, 28) != 1)
            {
                throw new Win32Exception();
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
    }
}
