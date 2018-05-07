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
			BxS_WorxExcel.UI.UC.WSConfigVM wsConfigVM1 = new BxS_WorxExcel.UI.UC.WSConfigVM();
			this.button1 = new System.Windows.Forms.Button();
			this.xcbx_Active = new System.Windows.Forms.CheckBox();
			this.UC_WSConfig = new BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig.UC_WSConfigVW();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
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
			this.xcbx_Active.Location = new System.Drawing.Point(3, 30);
			this.xcbx_Active.Name = "xcbx_Active";
			this.xcbx_Active.Size = new System.Drawing.Size(80, 17);
			this.xcbx_Active.TabIndex = 2;
			this.xcbx_Active.Text = "checkBox1";
			this.xcbx_Active.UseVisualStyleBackColor = true;
			// 
			// UC_WSConfig
			// 
			this.UC_WSConfig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UC_WSConfig.Location = new System.Drawing.Point(0, 0);
			this.UC_WSConfig.Name = "UC_WSConfig";
			this.UC_WSConfig.Size = new System.Drawing.Size(487, 246);
			this.UC_WSConfig.TabIndex = 3;
			wsConfigVM1.Active = false;
			wsConfigVM1.Col_Active = null;
			wsConfigVM1.Col_Exec = null;
			wsConfigVM1.Col_ID = null;
			wsConfigVM1.Col_Msg = null;
			wsConfigVM1.DataCol = null;
			wsConfigVM1.DataRow = 0;
			wsConfigVM1.DefSize = false;
			wsConfigVM1.DisMode = null;
			wsConfigVM1.GUID = new System.Guid("00000000-0000-0000-0000-000000000000");
			wsConfigVM1.Password = null;
			wsConfigVM1.PauseTime = 0;
			wsConfigVM1.Protected = false;
			wsConfigVM1.SAPTCode = null;
			wsConfigVM1.SessionID = null;
			wsConfigVM1.Skip1st = false;
			wsConfigVM1.UpdMode = '\0';
			this.UC_WSConfig.ViewModel = wsConfigVM1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.button1);
			this.splitContainer1.Panel1.Controls.Add(this.xcbx_Active);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.UC_WSConfig);
			this.splitContainer1.Size = new System.Drawing.Size(487, 318);
			this.splitContainer1.SplitterDistance = 68;
			this.splitContainer1.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 318);
			this.Controls.Add(this.splitContainer1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

			}

		#endregion
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox xcbx_Active;
		private UC_WSConfigVW UC_WSConfig;
		private System.Windows.Forms.SplitContainer splitContainer1;
		}
	}