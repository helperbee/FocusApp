using System;
using System.Diagnostics;
using System.Management;
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


        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        private const uint EVENT_SYSTEM_MINIMIZESTART = 22;
        private const uint EVENT_SYSTEM_MINIMIZEEND = 23;
        private IntPtr m_hhook;

        public Form1()
        {
            InitializeComponent();
            delegated = new WinEventDelegate(HandleEvent);
            m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_MINIMIZEEND, IntPtr.Zero, delegated, 0, 0, WINEVENT_OUTOFCONTEXT);
            imageList1.ImageSize = new Size(16, 16);

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
                try
                {
                    if (Program.session != null && !Program.session.IsSessionFinished() && Program.session.FindTarget(foreground) == null && foreground != this.Handle)
                        Helpers.SetForegroundWindow(Program.session.TargetList[0].Handle);
                    if (current != null && current.Handle != foreground)
                    {
                        current.End = DateTime.Now;
                        Debug.WriteLine(current.duration.ToString());
                        Program.Info.Add(new Info(current, createNew));
                    }
                    current = createNew;
                    Debug.WriteLine(foregroundTitle);
                    //Debug.WriteLine(String.Format("{0} - {1}", current.ProcessName, current.WindowTitle));
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

        }

        private Dictionary<string, Icon> ApplicationIcons = new Dictionary<string, Icon>(); //Testing memory issue
        private void LoadProcesses()
        {
            //This can be changed to use references
            processList.Invoke(new Action(() =>
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
                        try
                        {
                            string filePath = Helpers.GetProcessFilename(p);
                            if(!ApplicationIcons.ContainsKey(filePath)) {
                                Helpers.SHFILEINFO shinfo = new Helpers.SHFILEINFO();
                                IntPtr hIcon = Helpers.SHGetFileInfo(filePath, 0, out shinfo, (uint)Marshal.SizeOf(typeof(Helpers.SHFILEINFO)), Helpers.SHGFI_ICON | Helpers.SHGFI_SMALLICON);
                                if (hIcon != IntPtr.Zero)
                                {
                                    Icon icon = Icon.FromHandle(shinfo.hIcon);
                                    ApplicationIcons.Add(filePath, icon);                                   

                                }
                            }
                            if (ApplicationIcons.ContainsKey(filePath)) {//Change logic? 
                                imageList1.Images.Add(ApplicationIcons[filePath]);
                                item.ImageIndex = imageList1.Images.Count - 1; // Index of the last added icon in the ImageList
                            }


                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error getting Icon for : {windowTitle}");
                        }
                        item.Tag = p.MainWindowHandle;
                        if (Program.session != null && Program.session.FindTarget(p.MainWindowHandle) != null)
                            item.BackColor = Color.Green;
                        item.Text = p.ProcessName;
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, p.Id.ToString()));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, windowTitle));
                        processList.Items.Add(item);
                        
                    }
                }
                processList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                processList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                processList.EndUpdate();
            }));
        }
        private ManagementEventWatcher _watcher;
        private void InitializeProcessWatcher()
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace OR Win32_ProcessStopTrace");
            _watcher = new ManagementEventWatcher(query);
            _watcher.EventArrived += ProcessEventArrived;
            _watcher.Start();
        }

        private void ProcessEventArrived(object sender, EventArrivedEventArgs e)
        {
            string eventType = e.NewEvent.ClassPath.ClassName;
            if (eventType == "Win32_ProcessStartTrace" || eventType == "Win32_ProcessStopTrace")
            {
                LoadProcesses();
            }
            /*int processId = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value);
            Debug.WriteLine(e);
            string processName = eventType == "Win32_ProcessStartTrace"
                ? GetProcessName(processId)
                : "Unknown";

            ListViewItem item = new ListViewItem(new string[] { processId.ToString(), processName, eventType });

            if (processList.InvokeRequired)
            {
                processList.Invoke(new Action(() => processList.Items.Add(item)));
            }
            else
            {
                processList.Items.Add(item);
            }*/
        }

        private string GetProcessName(int processId)
        {
            string processName = "Unknown";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId = {processId}"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    processName = obj["Name"].ToString();
                    break;
                }
            }

            return processName;
        }
        private ListViewColumnSorter lvwColumnSorter;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProcesses();
            InitializeProcessWatcher();
            lvwColumnSorter = new ListViewColumnSorter();
            processList.ListViewItemSorter = lvwColumnSorter;
        }

        private void focusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(processList.SelectedItems.Count > 0)
            {
                List<Target> selectedItems = processList.SelectedItems.Cast<ListViewItem>().Select(item => new Target((IntPtr)item.Tag)).ToList();
                new Builder(selectedItems).ShowDialog();
                LoadProcesses();
            }
            /*foreach(ListViewItem p in processList.SelectedItems)
            {
                Debug.WriteLine(p.Tag);
                Program.TargetInfo.Handle = (IntPtr)p.Tag;
                this.Text = String.Format("Focusing - {0}", Program.TargetInfo.Handle);
            }*/

            
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Events(Helpers.InfoList(processList.SelectedItems)).ShowDialog();
            foreach(Info info in Program.Info)
            {
                Debug.WriteLine(String.Format("{0}({1}) -> {2}({3})", info.From.ProcessName, info.From.WindowTitle, info.To.ProcessName, info.To.WindowTitle));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _watcher?.Stop();
            UnhookWinEvent(m_hhook);
        }

        private void processList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            processList.Sort();
        }
    }
}