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

        private void buildBtn_click(object sender, EventArgs e)
        {
            //Build
            if (textBox1.Text.Length > 0)
            {
                Program.session = new Session(myTarget, (double)numericUpDown1.Value, textBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please give the session a name.", "MANDATORY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Builder_Load(object sender, EventArgs e)
        {
            textBox1.Text = $"Session #{Program.sessionStorage.Count + 1}";
            textBox1.Select();
        }
    }
}
