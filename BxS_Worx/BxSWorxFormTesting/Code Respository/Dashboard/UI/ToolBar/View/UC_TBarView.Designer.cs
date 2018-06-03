namespace BxS_Worx.Dashboard.UI.Toolbar
	{
	partial class UC_TBarView
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
			this.xpnl_Bar = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// xpnl_Bar
			// 
			this.xpnl_Bar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xpnl_Bar.Location = new System.Drawing.Point(0 , 0);
			this.xpnl_Bar.Name = "xpnl_Bar";
			this.xpnl_Bar.Size = new System.Drawing.Size(45 , 45);
			this.xpnl_Bar.TabIndex = 0;
			// 
			// UC_ToolBar
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.xpnl_Bar);
			this.Name = "UC_ToolBar";
			this.Size = new System.Drawing.Size(45 , 45);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_Bar;
		}
	}
