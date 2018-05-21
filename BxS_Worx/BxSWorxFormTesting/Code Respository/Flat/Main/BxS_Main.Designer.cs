namespace BxS_WorxExcel.UI.Forms
	{
	partial class BxS_Main
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BxS_Main));
			this.xpnl_WindowHeader = new System.Windows.Forms.Panel();
			this.xbtn_FormMinimise = new System.Windows.Forms.Button();
			this.xbtn_FormClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.xpnl_Menu = new System.Windows.Forms.Panel();
			this.button6 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.xbtn_Menu = new System.Windows.Forms.Button();
			this.xbtn_Settings = new System.Windows.Forms.Button();
			this.xpnl_SlidePanel = new System.Windows.Forms.Panel();
			this.xtmr_SlidePanel = new System.Windows.Forms.Timer(this.components);
			this.xspl_UC = new System.Windows.Forms.SplitContainer();
			this.xdlg_Colour = new System.Windows.Forms.ColorDialog();
			this.xpnl_WindowHeader.SuspendLayout();
			this.xpnl_Menu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xspl_UC)).BeginInit();
			this.xspl_UC.SuspendLayout();
			this.SuspendLayout();
			// 
			// xpnl_WindowHeader
			// 
			this.xpnl_WindowHeader.BackColor = System.Drawing.Color.Gray;
			this.xpnl_WindowHeader.Controls.Add(this.xbtn_FormMinimise);
			this.xpnl_WindowHeader.Controls.Add(this.xbtn_FormClose);
			this.xpnl_WindowHeader.Controls.Add(this.label1);
			this.xpnl_WindowHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.xpnl_WindowHeader.Location = new System.Drawing.Point(0, 0);
			this.xpnl_WindowHeader.Name = "xpnl_WindowHeader";
			this.xpnl_WindowHeader.Padding = new System.Windows.Forms.Padding(1);
			this.xpnl_WindowHeader.Size = new System.Drawing.Size(882, 45);
			this.xpnl_WindowHeader.TabIndex = 0;
			this.xpnl_WindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnWindowHeader_MouseDown);
			this.xpnl_WindowHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnWindowHeader_MouseMove);
			this.xpnl_WindowHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnWindowHeader_MouseUp);
			// 
			// xbtn_FormMinimise
			// 
			this.xbtn_FormMinimise.Dock = System.Windows.Forms.DockStyle.Right;
			this.xbtn_FormMinimise.FlatAppearance.BorderSize = 0;
			this.xbtn_FormMinimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xbtn_FormMinimise.Image = global::BxSWorxFormTesting.Properties.Resources.icons8_Minimize_Window_25px;
			this.xbtn_FormMinimise.Location = new System.Drawing.Point(795, 1);
			this.xbtn_FormMinimise.Margin = new System.Windows.Forms.Padding(0);
			this.xbtn_FormMinimise.Name = "xbtn_FormMinimise";
			this.xbtn_FormMinimise.Size = new System.Drawing.Size(43, 43);
			this.xbtn_FormMinimise.TabIndex = 2;
			this.xbtn_FormMinimise.UseVisualStyleBackColor = true;
			this.xbtn_FormMinimise.Click += new System.EventHandler(this.OnFormMinimise_Click);
			// 
			// xbtn_FormClose
			// 
			this.xbtn_FormClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.xbtn_FormClose.FlatAppearance.BorderSize = 0;
			this.xbtn_FormClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xbtn_FormClose.Image = global::BxSWorxFormTesting.Properties.Resources.icons8_Close_Window_25px;
			this.xbtn_FormClose.Location = new System.Drawing.Point(838, 1);
			this.xbtn_FormClose.Margin = new System.Windows.Forms.Padding(0);
			this.xbtn_FormClose.Name = "xbtn_FormClose";
			this.xbtn_FormClose.Size = new System.Drawing.Size(43, 43);
			this.xbtn_FormClose.TabIndex = 1;
			this.xbtn_FormClose.UseVisualStyleBackColor = true;
			this.xbtn_FormClose.Click += new System.EventHandler(this.OnFormClose_Click);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.Image = global::BxSWorxFormTesting.Properties.Resources.icons8_Unit_25px;
			this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label1.Location = new System.Drawing.Point(1, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 43);
			this.label1.TabIndex = 0;
			this.label1.Text = "BxS Dashboard";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// xpnl_Menu
			// 
			this.xpnl_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.xpnl_Menu.Controls.Add(this.button6);
			this.xpnl_Menu.Controls.Add(this.button2);
			this.xpnl_Menu.Controls.Add(this.xbtn_Menu);
			this.xpnl_Menu.Controls.Add(this.xbtn_Settings);
			this.xpnl_Menu.Dock = System.Windows.Forms.DockStyle.Left;
			this.xpnl_Menu.Location = new System.Drawing.Point(0, 45);
			this.xpnl_Menu.Name = "xpnl_Menu";
			this.xpnl_Menu.Padding = new System.Windows.Forms.Padding(1);
			this.xpnl_Menu.Size = new System.Drawing.Size(45, 536);
			this.xpnl_Menu.TabIndex = 1;
			// 
			// button6
			// 
			this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.button6.Dock = System.Windows.Forms.DockStyle.Top;
			this.button6.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.button6.FlatAppearance.BorderSize = 0;
			this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Indigo;
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
			this.button6.Location = new System.Drawing.Point(1, 118);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(43, 39);
			this.button6.TabIndex = 3;
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.button2.Dock = System.Windows.Forms.DockStyle.Top;
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.button2.FlatAppearance.BorderSize = 0;
			this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Indigo;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(1, 79);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(43, 39);
			this.button2.TabIndex = 2;
			this.button2.UseVisualStyleBackColor = true;
			// 
			// xbtn_Menu
			// 
			this.xbtn_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.xbtn_Menu.Dock = System.Windows.Forms.DockStyle.Top;
			this.xbtn_Menu.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.xbtn_Menu.FlatAppearance.BorderSize = 0;
			this.xbtn_Menu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Indigo;
			this.xbtn_Menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xbtn_Menu.Image = ((System.Drawing.Image)(resources.GetObject("xbtn_Menu.Image")));
			this.xbtn_Menu.Location = new System.Drawing.Point(1, 40);
			this.xbtn_Menu.Name = "xbtn_Menu";
			this.xbtn_Menu.Size = new System.Drawing.Size(43, 39);
			this.xbtn_Menu.TabIndex = 1;
			this.xbtn_Menu.UseVisualStyleBackColor = true;
			this.xbtn_Menu.Click += new System.EventHandler(this.Xbtn_Menu_Click);
			// 
			// xbtn_Settings
			// 
			this.xbtn_Settings.AutoSize = true;
			this.xbtn_Settings.Dock = System.Windows.Forms.DockStyle.Top;
			this.xbtn_Settings.FlatAppearance.BorderSize = 0;
			this.xbtn_Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xbtn_Settings.Image = global::BxSWorxFormTesting.Properties.Resources.icons8_Settings_25px;
			this.xbtn_Settings.Location = new System.Drawing.Point(1, 1);
			this.xbtn_Settings.Name = "xbtn_Settings";
			this.xbtn_Settings.Size = new System.Drawing.Size(43, 39);
			this.xbtn_Settings.TabIndex = 0;
			this.xbtn_Settings.UseVisualStyleBackColor = true;
			this.xbtn_Settings.Click += new System.EventHandler(this.Button1_Click);
			// 
			// xpnl_SlidePanel
			// 
			this.xpnl_SlidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.xpnl_SlidePanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.xpnl_SlidePanel.Location = new System.Drawing.Point(45, 45);
			this.xpnl_SlidePanel.Name = "xpnl_SlidePanel";
			this.xpnl_SlidePanel.Size = new System.Drawing.Size(45, 536);
			this.xpnl_SlidePanel.TabIndex = 2;
			// 
			// xtmr_SlidePanel
			// 
			this.xtmr_SlidePanel.Interval = 1;
			this.xtmr_SlidePanel.Tick += new System.EventHandler(this.OnSlidePanel_Tick);
			// 
			// xspl_UC
			// 
			this.xspl_UC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xspl_UC.Location = new System.Drawing.Point(90, 45);
			this.xspl_UC.Name = "xspl_UC";
			this.xspl_UC.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.xspl_UC.Size = new System.Drawing.Size(792, 536);
			this.xspl_UC.SplitterDistance = 263;
			this.xspl_UC.TabIndex = 4;
			// 
			// BxS_Main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.ClientSize = new System.Drawing.Size(882, 581);
			this.Controls.Add(this.xspl_UC);
			this.Controls.Add(this.xpnl_SlidePanel);
			this.Controls.Add(this.xpnl_Menu);
			this.Controls.Add(this.xpnl_WindowHeader);
			this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "BxS_Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BxS_Main";
			this.Load += new System.EventHandler(this.BxS_Main_Load);
			this.xpnl_WindowHeader.ResumeLayout(false);
			this.xpnl_Menu.ResumeLayout(false);
			this.xpnl_Menu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.xspl_UC)).EndInit();
			this.xspl_UC.ResumeLayout(false);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_WindowHeader;
		private System.Windows.Forms.Panel xpnl_Menu;
		private System.Windows.Forms.Panel xpnl_SlidePanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button xbtn_Menu;
		private System.Windows.Forms.Button xbtn_Settings;
		private System.Windows.Forms.Button xbtn_FormMinimise;
		private System.Windows.Forms.Button xbtn_FormClose;
		private System.Windows.Forms.Timer xtmr_SlidePanel;
		private System.Windows.Forms.SplitContainer xspl_UC;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.ColorDialog xdlg_Colour;
		}
	}