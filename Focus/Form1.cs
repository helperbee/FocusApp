using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Focus
{

    public partial class Form1 : Form
    {
        IntPtr targetId = IntPtr.Zero;
        WinEventDelegate delegated = null;
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        private const uint EVENT_SYSTEM_MINIMIZESTART = 22;
        private const uint EVENT_SYSTEM_MINIMIZEEND = 23;

        public static string GetText(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }
        public Form1()
        {
            InitializeComponent();
            delegated = new WinEventDelegate(HandleEvent);
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_MINIMIZEEND, IntPtr.Zero, delegated, 0, 0, WINEVENT_OUTOFCONTEXT);
        }
        public void HandleEvent(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {

            var foreground = GetForegroundWindow();
            var foregroundTitle = GetText(foreground);//I should probably do a title check instead of a process check for chrome
            Debug.WriteLine(String.Format("{0} - {1}", foreground, targetId));
            Debug.WriteLine(foregroundTitle);
            if (foreground != targetId && foreground != this.Handle)
            {
                SetForegroundWindow(targetId);
            }

        }

        private void LoadProcesses()
        {
            processList.BeginUpdate();
            processList.Items.Clear();
            var pList = Process.GetProcesses();
            foreach (var p in pList)
            {
                var windowTitle = GetText(p.MainWindowHandle);
                if (windowTitle.Length > 0)//Probably implement a check on window's processes.
                {
                    var item = new ListViewItem();
                    item.Tag = p.MainWindowHandle;
                    if (targetId == p.MainWindowHandle)
                        item.BackColor = Color.Green;
                    item.Text = p.Id.ToString();
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, p.ProcessName));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, windowTitle));
                    processList.Items.Add(item);
                }
            }
            processList.EndUpdate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void focusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem p in processList.SelectedItems)
            {
                Debug.WriteLine(p.Tag);
                targetId = (IntPtr)p.Tag;
                this.Text = String.Format("Focusing - {0}", targetId);
            }

            
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }
    }
}