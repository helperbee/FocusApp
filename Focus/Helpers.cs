using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    public class Helpers
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        public static string GetText(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        public static int GetProcessIdFromHandle(IntPtr handle)
        {
            try
            {
                int pId = 0;
                GetWindowThreadProcessId(handle, out pId);
                return pId;
/*                if (pInfo != null)
                {
                    Debug.WriteLine(String.Format("{0} - {1}", pInfo.ProcessName, pInfo.MainModule.FileName));
                    return pInfo.ProcessName;
                }*/
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return 0;
        }

        public static string GetProcessNameFromId(int processId)
        {
            try
            {
                var pInfo = Process.GetProcessById(processId);
                return pInfo.ProcessName;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return "Undefined";
        }

        public static bool GetIsSystemFile(Process targetProcess)//Surely there's a better way to do this
        {
            try
            {
                string systemFolderPath = Environment.GetEnvironmentVariable("SystemRoot");
                string mainModuleFilePath = targetProcess.MainModule.FileName;
                systemFolderPath = Path.GetFullPath(systemFolderPath).ToLower();
                mainModuleFilePath = Path.GetFullPath(mainModuleFilePath).ToLower();
                return mainModuleFilePath.StartsWith(systemFolderPath);
            }
            catch(Exception ex)
            {
                //Debug.WriteLine(ex);
            }
            return true;
        }
    }
}
