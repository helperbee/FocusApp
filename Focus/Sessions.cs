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
    public partial class Sessions : Form
    {
        public Sessions()
        {
            InitializeComponent();
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
           
            
            for (var x = 0; x < Program.sessionStorage.Count; x++) { 
                var session = Program.sessionStorage[x];
                List<string> processNames = new List<string>();//this is reused logic, keep dry later
                foreach (var inner in session.TargetList)
                {
                    if (!processNames.Contains(inner.ProcessName))
                        processNames.Add(inner.ProcessName);                    
                }
                var item = new ListViewItem();
                item.Text = session.Name;
                item.Tag = x;//Useful for viewsession logic.
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, string.Join(", ", processNames)));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, session.Minutes));
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void viewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                var targetSession = Program.sessionStorage[(int)listView1.SelectedItems[0].Tag];
                new ViewSession(targetSession).ShowDialog();
            }
        }
    }
}
