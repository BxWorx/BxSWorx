namespace BxS_Worx.UI.Dashboard
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
			this.xlbl_BtnText = new System.Windows.Forms.Label();
			this.xpnl_Button.SuspendLayout();
			this.SuspendLayout();
			// 
			// xpnl_Image
			// 
			this.xpnl_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.xpnl_Image.Dock = System.Windows.Forms.DockStyle.Left;
			this.xpnl_Image.Location = new System.Drawing.Point(0 , 0);
			this.xpnl_Image.Name = "xpnl_Image";
			this.xpnl_Image.Size = new System.Drawing.Size(45 , 45);
			this.xpnl_Image.TabIndex = 0;
			// 
			// xpnl_Button
			// 
			this.xpnl_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.xpnl_Button.Controls.Add(this.xlbl_BtnText);
			this.xpnl_Button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xpnl_Button.Font = new System.Drawing.Font("Microsoft Sans Serif" , 9.75F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ((byte) (0)));
			this.xpnl_Button.Location = new System.Drawing.Point(45 , 0);
			this.xpnl_Button.Margin = new System.Windows.Forms.Padding(0);
			this.xpnl_Button.Name = "xpnl_Button";
			this.xpnl_Button.Size = new System.Drawing.Size(135 , 45);
			this.xpnl_Button.TabIndex = 1;
			// 
			// xlbl_BtnText
			// 
			this.xlbl_BtnText.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.xlbl_BtnText.BackColor = System.Drawing.Color.Transparent;
			this.xlbl_BtnText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xlbl_BtnText.Location = new System.Drawing.Point(6 , 10);
			this.xlbl_BtnText.Name = "xlbl_BtnText";
			this.xlbl_BtnText.Size = new System.Drawing.Size(126 , 25);
			this.xlbl_BtnText.TabIndex = 0;
			this.xlbl_BtnText.Text = "label1";
			this.xlbl_BtnText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UC_BtnFlipFlop
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.xpnl_Button);
			this.Controls.Add(this.xpnl_Image);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_BtnFlipFlop";
			this.Size = new System.Drawing.Size(180 , 45);
			this.Load += new System.EventHandler(this.OnLoad);
			this.xpnl_Button.ResumeLayout(false);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_Image;
		private System.Windows.Forms.Panel xpnl_Button;
		private System.Windows.Forms.Label xlbl_BtnText;
		}
	}
