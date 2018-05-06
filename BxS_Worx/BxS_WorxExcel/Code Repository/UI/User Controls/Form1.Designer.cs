namespace BxS_WorxExcel.Code_Repository.UI.User_Controls
	{
	partial class Form1
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
			if ( disposing && ( components != null ) )
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
			this.xUC_WSConfig = new BxS_WorxExcel.Code_Repository.UI.User_Controls.UC_Config();
			this.button1 = new System.Windows.Forms.Button();
			this.xtbx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// xUC_WSConfig
			// 
			this.xUC_WSConfig.Location = new System.Drawing.Point(12, 12);
			this.xUC_WSConfig.Name = "xUC_WSConfig";
			this.xUC_WSConfig.Size = new System.Drawing.Size(281, 35);
			this.xUC_WSConfig.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 53);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// xtbx
			// 
			this.xtbx.Location = new System.Drawing.Point(93, 53);
			this.xtbx.Name = "xtbx";
			this.xtbx.Size = new System.Drawing.Size(227, 20);
			this.xtbx.TabIndex = 2;
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(358, 113);
			this.Controls.Add(this.xtbx);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.xUC_WSConfig);
			this.Name = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private UC_Config xUC_WSConfig;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox xtbx;
		}
	}