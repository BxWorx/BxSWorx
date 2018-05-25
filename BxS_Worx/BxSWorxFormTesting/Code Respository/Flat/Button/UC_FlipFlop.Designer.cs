namespace BxSWorxFormTesting.Code_Respository.Flat.Button
	{
	partial class UC_FlipFlop
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
			this.xpnl_Button = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// xpnl_Button
			// 
			this.xpnl_Button.BackColor = System.Drawing.Color.Maroon;
			this.xpnl_Button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xpnl_Button.Location = new System.Drawing.Point(50, 0);
			this.xpnl_Button.Margin = new System.Windows.Forms.Padding(0);
			this.xpnl_Button.Name = "xpnl_Button";
			this.xpnl_Button.Size = new System.Drawing.Size(100, 63);
			this.xpnl_Button.TabIndex = 0;
			this.xpnl_Button.Click += new System.EventHandler(this.OnClick);
			// 
			// UC_FlipFlop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.xpnl_Button);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_FlipFlop";
			this.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
			this.Size = new System.Drawing.Size(200, 63);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_Button;
		}
	}
