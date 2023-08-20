namespace Focus
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.processList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.mainStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.focusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastSessionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endEarlyTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // processList
            // 
            this.processList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.processList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.processList.ContextMenuStrip = this.mainStrip;
            this.processList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processList.FullRowSelect = true;
            this.processList.Location = new System.Drawing.Point(0, 0);
            this.processList.Name = "processList";
            this.processList.Size = new System.Drawing.Size(800, 450);
            this.processList.SmallImageList = this.imageList1;
            this.processList.TabIndex = 0;
            this.processList.UseCompatibleStateImageBehavior = false;
            this.processList.View = System.Windows.Forms.View.Details;
            this.processList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.processList_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Process";
            this.columnHeader1.Width = 175;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "PID";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Window Title";
            this.columnHeader3.Width = 300;
            // 
            // mainStrip
            // 
            this.mainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.focusToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.pastSessionsToolStripMenuItem,
            this.endEarlyTestToolStripMenuItem});
            this.mainStrip.Name = "mainStrip";
            this.mainStrip.Size = new System.Drawing.Size(137, 114);
            // 
            // focusToolStripMenuItem
            // 
            this.focusToolStripMenuItem.Name = "focusToolStripMenuItem";
            this.focusToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.focusToolStripMenuItem.Text = "Focus";
            this.focusToolStripMenuItem.Click += new System.EventHandler(this.focusToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // pastSessionsToolStripMenuItem
            // 
            this.pastSessionsToolStripMenuItem.Name = "pastSessionsToolStripMenuItem";
            this.pastSessionsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.pastSessionsToolStripMenuItem.Text = "Sessions";
            this.pastSessionsToolStripMenuItem.Click += new System.EventHandler(this.pastSessionsToolStripMenuItem_Click);
            // 
            // endEarlyTestToolStripMenuItem
            // 
            this.endEarlyTestToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.endEarlyTestToolStripMenuItem.Name = "endEarlyTestToolStripMenuItem";
            this.endEarlyTestToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.endEarlyTestToolStripMenuItem.Text = "End Session";
            this.endEarlyTestToolStripMenuItem.Click += new System.EventHandler(this.endEarlyTestToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.processList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Focus Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListView processList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ContextMenuStrip mainStrip;
        private ToolStripMenuItem focusToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ImageList imageList1;
        private ToolStripMenuItem pastSessionsToolStripMenuItem;
        private ToolStripMenuItem endEarlyTestToolStripMenuItem;
    }
}