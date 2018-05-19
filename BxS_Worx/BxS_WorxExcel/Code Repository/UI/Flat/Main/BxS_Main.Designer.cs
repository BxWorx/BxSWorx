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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.xpnl_SlidePanel = new System.Windows.Forms.Panel();
			this.xpnl_UC = new System.Windows.Forms.Panel();
			this.xtmr_SlidePanel = new System.Windows.Forms.Timer(this.components);
			this.xbtn_Menu = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(1);
			this.panel1.Size = new System.Drawing.Size(882, 32);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "Dashboard";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.xbtn_Menu);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 32);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(1);
			this.panel2.Size = new System.Drawing.Size(45, 549);
			this.panel2.TabIndex = 1;
			// 
			// xpnl_SlidePanel
			// 
			this.xpnl_SlidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.xpnl_SlidePanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.xpnl_SlidePanel.Location = new System.Drawing.Point(45, 32);
			this.xpnl_SlidePanel.Name = "xpnl_SlidePanel";
			this.xpnl_SlidePanel.Size = new System.Drawing.Size(230, 549);
			this.xpnl_SlidePanel.TabIndex = 2;
			// 
			// xpnl_UC
			// 
			this.xpnl_UC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xpnl_UC.Location = new System.Drawing.Point(275, 32);
			this.xpnl_UC.Name = "xpnl_UC";
			this.xpnl_UC.Size = new System.Drawing.Size(607, 549);
			this.xpnl_UC.TabIndex = 3;
			// 
			// xtmr_SlidePanel
			// 
			this.xtmr_SlidePanel.Interval = 1;
			this.xtmr_SlidePanel.Tick += new System.EventHandler(this.Xtmr_SlidePanel_Tick);
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
			this.xbtn_Menu.UseVisualStyleBackColor = false;
			this.xbtn_Menu.Click += new System.EventHandler(this.Xbtn_Menu_Click);
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.Dock = System.Windows.Forms.DockStyle.Top;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(1, 1);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(43, 39);
			this.button1.TabIndex = 0;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button4
			// 
			this.button4.Dock = System.Windows.Forms.DockStyle.Right;
			this.button4.FlatAppearance.BorderSize = 0;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
			this.button4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.button4.Location = new System.Drawing.Point(831, 1);
			this.button4.Margin = new System.Windows.Forms.Padding(0);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(25, 30);
			this.button4.TabIndex = 2;
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Dock = System.Windows.Forms.DockStyle.Right;
			this.button3.FlatAppearance.BorderSize = 0;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
			this.button3.Location = new System.Drawing.Point(856, 1);
			this.button3.Margin = new System.Windows.Forms.Padding(0);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(25, 30);
			this.button3.TabIndex = 1;
			this.button3.UseVisualStyleBackColor = true;
			// 
			// BxS_Main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.ClientSize = new System.Drawing.Size(882, 581);
			this.Controls.Add(this.xpnl_UC);
			this.Controls.Add(this.xpnl_SlidePanel);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "BxS_Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BxS_Main";
			this.Load += new System.EventHandler(this.BxS_Main_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel xpnl_SlidePanel;
		private System.Windows.Forms.Panel xpnl_UC;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button xbtn_Menu;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Timer xtmr_SlidePanel;
		}
	}