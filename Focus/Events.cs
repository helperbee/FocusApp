using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Focus
{
    public partial class Events : Form
    {
        private List<Info> _events;
        public Events(List<Info> InfoList)
        {
            _events = InfoList;
            lvwColumnSorter = new ListViewColumnSorter();
            InitializeComponent();
        }
        private ListViewColumnSorter lvwColumnSorter;
        private void Events_Load(object sender, EventArgs e)
        {
            //
            this.listView1.ListViewItemSorter = lvwColumnSorter;
            foreach (Info info in _events)
            {
                var item = new ListViewItem();
                item.Text = info.From.ProcessName;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, info.From.WindowTitle));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, info.To.ProcessName));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, info.To.WindowTitle));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, info.From.duration.ToString()));
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
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
            this.listView1.Sort();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C)
            {
                StringBuilder toClipboard = new StringBuilder();
                double totalSeconds = 0;
                List<string> processNames = new List<string>();
                //fix time adding..
                foreach (Info info in _events)
                {
                    totalSeconds += info.From.duration.TotalSeconds;
                    if(!processNames.Contains(info.From.ProcessName))
                        processNames.Add(info.From.ProcessName);

                    //toClipboard.AppendLine(String.Format("{0}->{1} | {2}", info.From.ProcessName, info.To.ProcessName, info.From.duration.ToString(@"d\.hh\:mm\:ss")));                  
                }
                TimeSpan totalDuration = TimeSpan.FromSeconds(totalSeconds);
                string formattedTotalDuration = string.Format("{0:D2}:{1:D2}:{2:D2}", totalDuration.Hours, totalDuration.Minutes, totalDuration.Seconds);

                toClipboard.AppendLine(String.Format("Total Time Spent in {0} : {1}", string.Join(", ", processNames), formattedTotalDuration));
                Clipboard.SetDataObject(toClipboard.ToString());//Set to computer's clipboard
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            StringBuilder toClipboard = new StringBuilder();
            double totalSeconds = 0;
            List<string> processNames = new List<string>();
            //fix time adding..
            foreach (Info info in _events)
            {
                totalSeconds += info.From.duration.TotalSeconds;
                if (!processNames.Contains(info.From.ProcessName))
                    processNames.Add(info.From.ProcessName);

                //toClipboard.AppendLine(String.Format("{0}->{1} | {2}", info.From.ProcessName, info.To.ProcessName, info.From.duration.ToString(@"d\.hh\:mm\:ss")));                  
            }
            TimeSpan totalDuration = TimeSpan.FromSeconds(totalSeconds);
            string formattedTotalDuration = string.Format("{0:D2}:{1:D2}:{2:D2}", totalDuration.Hours, totalDuration.Minutes, totalDuration.Seconds);

            toClipboard.AppendLine(String.Format("Total Time Spent in {0} : {1}", string.Join(", ", processNames), formattedTotalDuration));
            //toolStripStatusLabel1
            toolStripStatusLabel1.Text = $"{toClipboard.ToString()}";
        }
    }
}
