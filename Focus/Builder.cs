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
    public partial class Builder : Form
    {
        private List<Target> myTarget;
        public Builder(List<Target> potentialTarget)
        {
            myTarget = potentialTarget;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Build
            Program.session = new Session(myTarget, (double)numericUpDown1.Value);
            this.Close();
        }
    }
}
