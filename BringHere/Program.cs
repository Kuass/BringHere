using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BringHere
{
    public class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int width, int height, uint uFlags);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        const uint SHOWWINDOW = 0x0040;

        static void Main(string[] args) {
            Console.Write("Enter Process Name : ");
            string process_name = Console.ReadLine();

            Console.WriteLine("Searching for a process named " + process_name + "...");
            Process[] processList = Process.GetProcessesByName(process_name);

            if (processList.Length >= 1) {
                RECT rect;
                GetWindowRect(new HandleRef(null, processList[0].MainWindowHandle), out rect);
                Console.WriteLine("Find Process.");
                Console.WriteLine("Process Pixel Right : " + rect.Right);
                Console.WriteLine("Process Pixel Left : " + rect.Left);
                Console.WriteLine("Process Pixel Top : " + rect.Top);
                Console.WriteLine("Process Pixel Bottom : " + rect.Bottom);
                Console.WriteLine("Form Size (W) : " + (rect.Right - rect.Left));
                Console.WriteLine("Form Size (H) : " + (rect.Bottom - rect.Top));

                Console.WriteLine("Continue bring to position set zero.");
                Console.ReadLine();

                SetWindowPos(processList[0].MainWindowHandle, processList[0].Handle, 0, 0, (rect.Right - rect.Left), (rect.Bottom - rect.Top), SHOWWINDOW);
            } else {
                Console.Write("Can't Find Process.");
            }

            Console.WriteLine("Any key press to Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
