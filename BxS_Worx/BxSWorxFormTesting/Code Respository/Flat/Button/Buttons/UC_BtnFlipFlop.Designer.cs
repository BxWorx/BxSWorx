namespace BxS_WorxExcel.UI.UC
	{
	partial class UC_BtnFlipFlop
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
			this.xpnl_Image = new System.Windows.Forms.Panel();
			this.xpnl_Button = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// xpnl_Image
			// 
			this.xpnl_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.xpnl_Image.Dock = System.Windows.Forms.DockStyle.Left;
			this.xpnl_Image.Location = new System.Drawing.Point(0, 0);
			this.xpnl_Image.Name = "xpnl_Image";
			this.xpnl_Image.Size = new System.Drawing.Size(45, 45);
			this.xpnl_Image.TabIndex = 0;
			// 
			// xpnl_Button
			// 
			this.xpnl_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.xpnl_Button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xpnl_Button.Location = new System.Drawing.Point(45, 0);
			this.xpnl_Button.Margin = new System.Windows.Forms.Padding(0);
			this.xpnl_Button.Name = "xpnl_Button";
			this.xpnl_Button.Size = new System.Drawing.Size(135, 45);
			this.xpnl_Button.TabIndex = 1;
			// 
			// UC_BtnFlipFlop
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.xpnl_Button);
			this.Controls.Add(this.xpnl_Image);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_BtnFlipFlop";
			this.Size = new System.Drawing.Size(180, 45);
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_Image;
		private System.Windows.Forms.Panel xpnl_Button;
		}
	}
