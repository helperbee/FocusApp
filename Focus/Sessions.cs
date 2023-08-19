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
    public partial class Sessions : Form
    {
        public Sessions()
        {
            InitializeComponent();
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
            foreach(var x in Program.session.AttemptedList)
            {
                var item = new ListViewItem();
                item.Text = x.WindowTitle;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, x.At.ToShortTimeString()));
                listView1.Items.Add(item);//could add range here
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
