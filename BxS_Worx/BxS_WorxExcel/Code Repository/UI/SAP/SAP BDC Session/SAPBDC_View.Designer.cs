namespace BxS_WorxExcel.UI.Forms
	{
	partial class SAPBDC_View
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if ( disposing && (components != null) )
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SAPBDC_View));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.xdtp_End = new System.Windows.Forms.DateTimePicker();
			this.xdtp_Start = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.xtbx_User = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.xtbx_SsnID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.xdgv_Sessions = new System.Windows.Forms.DataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.xbtn_Load = new System.Windows.Forms.ToolStripButton();
			this.xbtn_Reset = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.xbtn_Previous = new System.Windows.Forms.ToolStripButton();
			this.xbtn_Save = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xdgv_Sessions)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(418, 279);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(418, 304);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.TabStop = false;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.xdgv_Sessions);
			this.splitContainer1.Size = new System.Drawing.Size(418, 279);
			this.splitContainer1.SplitterDistance = 79;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.xdtp_End);
			this.panel1.Controls.Add(this.xdtp_Start);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.xtbx_User);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.xtbx_SsnID);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(418, 79);
			this.panel1.TabIndex = 1;
			// 
			// xdtp_End
			// 
			this.xdtp_End.Location = new System.Drawing.Point(245, 40);
			this.xdtp_End.Name = "xdtp_End";
			this.xdtp_End.Size = new System.Drawing.Size(147, 20);
			this.xdtp_End.TabIndex = 2;
			// 
			// xdtp_Start
			// 
			this.xdtp_Start.Checked = false;
			this.xdtp_Start.Location = new System.Drawing.Point(245, 11);
			this.xdtp_Start.Name = "xdtp_Start";
			this.xdtp_Start.Size = new System.Drawing.Size(147, 20);
			this.xdtp_Start.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(209, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "To";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(209, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "From";
			// 
			// xtbx_User
			// 
			this.xtbx_User.Location = new System.Drawing.Point(80, 14);
			this.xtbx_User.Name = "xtbx_User";
			this.xtbx_User.Size = new System.Drawing.Size(100, 20);
			this.xtbx_User.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Session ID";
			// 
			// xtbx_SsnID
			// 
			this.xtbx_SsnID.Location = new System.Drawing.Point(80, 43);
			this.xtbx_SsnID.Name = "xtbx_SsnID";
			this.xtbx_SsnID.Size = new System.Drawing.Size(100, 20);
			this.xtbx_SsnID.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "User Name";
			// 
			// xdgv_Sessions
			// 
			this.xdgv_Sessions.AllowUserToAddRows = false;
			this.xdgv_Sessions.AllowUserToDeleteRows = false;
			this.xdgv_Sessions.AllowUserToOrderColumns = true;
			this.xdgv_Sessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.xdgv_Sessions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xdgv_Sessions.Location = new System.Drawing.Point(0, 0);
			this.xdgv_Sessions.Name = "xdgv_Sessions";
			this.xdgv_Sessions.ReadOnly = true;
			this.xdgv_Sessions.Size = new System.Drawing.Size(418, 196);
			this.xdgv_Sessions.TabIndex = 0;
			this.xdgv_Sessions.Visible = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xbtn_Load,
            this.xbtn_Reset,
            this.toolStripSeparator1,
            this.xbtn_Previous,
            this.xbtn_Save});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip1.Size = new System.Drawing.Size(131, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// xtbn_Load
			// 
			this.xbtn_Load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.xbtn_Load.Image = global::BxS_WorxExcel.Properties.Resources.if_table_excel_64759;
			this.xbtn_Load.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.xbtn_Load.Name = "xtbn_Load";
			this.xbtn_Load.Size = new System.Drawing.Size(23, 22);
			this.xbtn_Load.Text = "toolStripButton1";
			// 
			// xbtn_Reset
			// 
			this.xbtn_Reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.xbtn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("xbtn_Reset.Image")));
			this.xbtn_Reset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.xbtn_Reset.Name = "xbtn_Reset";
			this.xbtn_Reset.Size = new System.Drawing.Size(23, 22);
			this.xbtn_Reset.Text = "toolStripButton2";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// xbtn_Previous
			// 
			this.xbtn_Previous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.xbtn_Previous.Image = ((System.Drawing.Image)(resources.GetObject("xbtn_Previous.Image")));
			this.xbtn_Previous.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.xbtn_Previous.Name = "xbtn_Previous";
			this.xbtn_Previous.Size = new System.Drawing.Size(23, 22);
			this.xbtn_Previous.Text = "toolStripButton3";
			// 
			// xbtn_Save
			// 
			this.xbtn_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.xbtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("xbtn_Save.Image")));
			this.xbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.xbtn_Save.Name = "xbtn_Save";
			this.xbtn_Save.Size = new System.Drawing.Size(23, 22);
			this.xbtn_Save.Text = "toolStripButton4";
			// 
			// SAPBDC_View
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 304);
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "SAPBDC_View";
			this.Text = "SAP BDC Sessions";
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.xdgv_Sessions)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		internal System.Windows.Forms.DataGridView xdgv_Sessions;
		internal System.Windows.Forms.DateTimePicker xdtp_End;
		internal System.Windows.Forms.DateTimePicker xdtp_Start;
		internal System.Windows.Forms.TextBox xtbx_SsnID;
		internal System.Windows.Forms.TextBox xtbx_User;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.ToolStripButton xbtn_Reset;
		internal System.Windows.Forms.ToolStripButton xbtn_Previous;
		internal System.Windows.Forms.ToolStripButton xbtn_Save;
		internal System.Windows.Forms.ToolStripButton xbtn_Load;
		}
	}