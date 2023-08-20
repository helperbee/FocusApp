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
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, string.Join(", ", processNames)));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, session.Minutes));
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


        }
    }
}
