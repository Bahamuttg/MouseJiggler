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
