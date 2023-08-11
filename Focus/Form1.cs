using System.Diagnostics;

namespace Focus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var pList = Process.GetProcesses();
            foreach(var p in pList)
            {
                var item = new ListViewItem();
                item.Text = p.ProcessName;
                processList.Items.Add(item);
            }
        }
    }
}