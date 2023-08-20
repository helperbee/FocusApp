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
    public partial class ViewSession : Form
    {
        private Session targetSession;
        public ViewSession(Session session)
        {
            targetSession = session;
            InitializeComponent();
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
            this.Text = $"Overview for Session '{targetSession.Name}'";
            foreach(var x in targetSession.AttemptedList)
            {
                var item = new ListViewItem();
                item.Text = x.ProcessName;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, x.WindowTitle));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, x.At.ToShortTimeString()));
                listView1.Items.Add(item);//could add range here
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
