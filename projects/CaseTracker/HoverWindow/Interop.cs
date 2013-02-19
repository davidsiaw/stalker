using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace FogBugzCaseTracker
{
    class Interop
    {
        public struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32")]
        static extern int BringWindowToTop(int hwnd);
        [DllImport("user32")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static TimeSpan GetTimeSinceLastInput()
        {
            // Get the system uptime
            int systemUptime = Environment.TickCount;
            // The MilliSecond at which the last input was recorded
            int LastInputMilli = 0;
            // The number of MilliSeconds that passed since last input
            int IdleMilli = 0;

            // Set the struct
            Interop.LASTINPUTINFO LastInputInfo = new Interop.LASTINPUTINFO();
            LastInputInfo.cbSize = (uint)Marshal.SizeOf(LastInputInfo);
            LastInputInfo.dwTime = 0;

            // If we have a value from the function
            if (GetLastInputInfo(ref LastInputInfo))
            {
                // Get the number of MilliSeconds at the point when the last activity was seen
                LastInputMilli = (int)LastInputInfo.dwTime;
                // Number of idle MilliSeconds = system uptime MilliSeconds - number of MilliSeconds at last input
                IdleMilli = systemUptime - LastInputMilli;
                return TimeSpan.FromMilliseconds(IdleMilli);
            }
            return TimeSpan.FromMilliseconds(0);
        }
    }
}
