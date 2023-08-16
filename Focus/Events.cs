using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            InitializeComponent();
        }
        private ListViewColumnSorter lvwColumnSorter;
        private void Events_Load(object sender, EventArgs e)
        {
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
            foreach (Info info in _events)
            {
                var item = new ListViewItem();
                item.Text = String.Format("({0}){1}", info.From.ProcessName, info.From.WindowTitle);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, String.Format("({0}){1}", info.To.ProcessName, info.To.WindowTitle)));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, info.From.duration.ToString()));
                listView1.Items.Add(item);
            }
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
    }
}
