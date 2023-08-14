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
        public Events()
        {
            InitializeComponent();
        }
        private void Events_Load(object sender, EventArgs e)
        {
            foreach(Info info in Program.Info)
            {
                var item = new ListViewItem();
                item.Text = String.Format("({0}){1}", info.From.ProcessName, info.From.WindowTitle);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, String.Format("({0}){1}", info.To.ProcessName, info.To.WindowTitle)));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, info.From.duration.ToString()));
                listView1.Items.Add(item);
            }
        }
    }
}
