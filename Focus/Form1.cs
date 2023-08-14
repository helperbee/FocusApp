using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Focus
{

    public partial class Form1 : Form
    {
        WinEventDelegate delegated = null;
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        
       
        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        private const uint EVENT_SYSTEM_MINIMIZESTART = 22;
        private const uint EVENT_SYSTEM_MINIMIZEEND = 23;

        public Form1()
        {
            InitializeComponent();
            delegated = new WinEventDelegate(HandleEvent);
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_MINIMIZEEND, IntPtr.Zero, delegated, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        ProcessInfo current;
        public void HandleEvent(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {

            var foreground = Helpers.GetForegroundWindow();
            var foregroundProcessId = Helpers.GetProcessIdFromHandle(foreground);
            var foregroundProcess = Process.GetProcessById(foregroundProcessId);
            var foregroundTitle = Helpers.GetText(foreground);//I should probably do a title check instead of a process check for chrome
            if (foregroundProcess != null && !Helpers.GetIsSystemFile(foregroundProcess))
            {
                var createNew = new ProcessInfo(foreground);
                if (foreground != Program.TargetInfo.Handle && foreground != this.Handle)
                    Helpers.SetForegroundWindow(Program.TargetInfo.Handle);
                if (current != null && current.Handle != foreground)
                    Program.Info.Add(new Info(current, createNew));
                current = createNew;
                Debug.WriteLine(foregroundTitle);
                //Debug.WriteLine(String.Format("{0} - {1}", current.ProcessName, current.WindowTitle));
            }

        }

        private void LoadProcesses()
        {
            processList.BeginUpdate();
            processList.Items.Clear();
            var pList = Process.GetProcesses();
            foreach (var p in pList)
            {
                var windowTitle = Helpers.GetText(p.MainWindowHandle);
                if (windowTitle.Length > 0)//Probably implement a check on window's processes.
                {
                    var item = new ListViewItem();
                    item.Tag = p.MainWindowHandle;
                    if (Program.TargetInfo.Handle == p.MainWindowHandle)
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
                Program.TargetInfo.Handle = (IntPtr)p.Tag;
                this.Text = String.Format("Focusing - {0}", Program.TargetInfo.Handle);
            }

            
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Info info in Program.Info)
            {
                Debug.WriteLine(String.Format("{0}({1}) -> {2}({3})", info.From.ProcessName, info.From.WindowTitle, info.To.ProcessName, info.To.WindowTitle));
            }
        }
    }
}