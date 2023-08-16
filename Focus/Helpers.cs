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
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
        string pszPath,
        uint dwFileAttributes,
        out SHFILEINFO psfi,
        uint cbFileInfo,
        uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;
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

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            QueryLimitedInformation = 0x00001000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool QueryFullProcessImageName(
              [In] IntPtr hProcess,
              [In] int dwFlags,
              [Out] StringBuilder lpExeName,
              ref int lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
         ProcessAccessFlags processAccess,
         bool bInheritHandle,
         int processId);
        public static string GetText(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }
        public static string GetProcessFilename(Process p)
        {
            int capacity = 2000;
            StringBuilder builder = new StringBuilder(capacity);
            IntPtr ptr = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, p.Id);
            if (!QueryFullProcessImageName(ptr, 0, builder, ref capacity))
            {
                return String.Empty;
            }

            return builder.ToString();
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
                string mainModuleFilePath = GetProcessFilename(targetProcess);
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

        public static List<Info> InfoList(ListView.SelectedListViewItemCollection selectedItems)
        {            
            var outList = new List<Info>();
            if (selectedItems.Count > 0)
            {
                //this has to be bad logic
                foreach(ListViewItem item in selectedItems)
                {
                    foreach(Info info in Program.Info)
                    {
                        if(info.From.Handle == (IntPtr)item.Tag)
                            outList.Add(info);
                    }
                }
            }
            else
                return Program.Info;
            return outList;
        }

    }
}
