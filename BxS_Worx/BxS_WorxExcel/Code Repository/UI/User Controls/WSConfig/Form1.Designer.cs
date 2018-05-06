namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
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
			BxS_WorxExcel.UI.UC.WSConfigVM wsConfigVM2 = new BxS_WorxExcel.UI.UC.WSConfigVM();
			this.button1 = new System.Windows.Forms.Button();
			this.xcbx_Active = new System.Windows.Forms.CheckBox();
			this.UC_WSConfig = new BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig.UC_WSConfigVW();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 123);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// xcbx_Active
			// 
			this.xcbx_Active.AutoSize = true;
			this.xcbx_Active.Location = new System.Drawing.Point(171, 126);
			this.xcbx_Active.Name = "xcbx_Active";
			this.xcbx_Active.Size = new System.Drawing.Size(80, 17);
			this.xcbx_Active.TabIndex = 2;
			this.xcbx_Active.Text = "checkBox1";
			this.xcbx_Active.UseVisualStyleBackColor = true;
			// 
			// UC_WSConfig
			// 
			this.UC_WSConfig.Location = new System.Drawing.Point(16, 14);
			this.UC_WSConfig.Name = "UC_WSConfig";
			this.UC_WSConfig.Size = new System.Drawing.Size(332, 106);
			this.UC_WSConfig.TabIndex = 3;
			wsConfigVM2.Active = false;
			wsConfigVM2.Col_Active = null;
			wsConfigVM2.Col_Exec = null;
			wsConfigVM2.Col_ID = null;
			wsConfigVM2.Col_Msg = null;
			wsConfigVM2.DataCol = null;
			wsConfigVM2.DataRow = 0;
			wsConfigVM2.DefSize = '\0';
			wsConfigVM2.DisMode = '\0';
			wsConfigVM2.GUID = new System.Guid("00000000-0000-0000-0000-000000000000");
			wsConfigVM2.Password = null;
			wsConfigVM2.PauseTime = 0;
			wsConfigVM2.Protected = false;
			wsConfigVM2.SAPTCode = null;
			wsConfigVM2.SessionID = null;
			wsConfigVM2.Skip1st = false;
			wsConfigVM2.UpdMode = '\0';
			this.UC_WSConfig.ViewModel = wsConfigVM2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 237);
			this.Controls.Add(this.UC_WSConfig);
			this.Controls.Add(this.xcbx_Active);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox xcbx_Active;
		private UC_WSConfigVW UC_WSConfig;
		}
	}