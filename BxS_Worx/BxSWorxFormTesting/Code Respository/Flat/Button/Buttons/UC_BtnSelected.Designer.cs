namespace BxS_WorxExcel.UI.UC
	{
	partial class UC_BtnSelected
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			this.xpnl_Selected = new System.Windows.Forms.Panel();
			this.xpnl_Button = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// xpnl_Selected
			// 
			this.xpnl_Selected.BackColor = System.Drawing.Color.Transparent;
			this.xpnl_Selected.Dock = System.Windows.Forms.DockStyle.Right;
			this.xpnl_Selected.Location = new System.Drawing.Point(45, 0);
			this.xpnl_Selected.Margin = new System.Windows.Forms.Padding(0);
			this.xpnl_Selected.Name = "xpnl_Selected";
			this.xpnl_Selected.Size = new System.Drawing.Size(3, 45);
			this.xpnl_Selected.TabIndex = 1;
			// 
			// xpnl_Button
			// 
			this.xpnl_Button.BackColor = System.Drawing.Color.Transparent;
			this.xpnl_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.xpnl_Button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xpnl_Button.Location = new System.Drawing.Point(0, 0);
			this.xpnl_Button.Margin = new System.Windows.Forms.Padding(0);
			this.xpnl_Button.Name = "xpnl_Button";
			this.xpnl_Button.Size = new System.Drawing.Size(45, 45);
			this.xpnl_Button.TabIndex = 2;
			// 
			// UC_BtnSelected
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.xpnl_Button);
			this.Controls.Add(this.xpnl_Selected);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_BtnSelected";
			this.Size = new System.Drawing.Size(48, 45);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_Selected;
		private System.Windows.Forms.Panel xpnl_Button;
		}
	}
