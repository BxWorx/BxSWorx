namespace BxS_WorxExcel.Code_Repository.UI.User_Controls
	{
	partial class UC_Config
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			this.xBxS_GUID = new System.Windows.Forms.TextBox();
			this.xbtn_NewGUID = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// xBxS_GUID
			// 
			this.xBxS_GUID.Location = new System.Drawing.Point(3, 3);
			this.xBxS_GUID.MaxLength = 40;
			this.xBxS_GUID.Name = "xBxS_GUID";
			this.xBxS_GUID.ReadOnly = true;
			this.xBxS_GUID.Size = new System.Drawing.Size(236, 20);
			this.xBxS_GUID.TabIndex = 0;
			this.xBxS_GUID.TabStop = false;
			// 
			// xbtn_NewGUID
			// 
			this.xbtn_NewGUID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.xbtn_NewGUID.Location = new System.Drawing.Point(245, 3);
			this.xbtn_NewGUID.Name = "xbtn_NewGUID";
			this.xbtn_NewGUID.Size = new System.Drawing.Size(26, 23);
			this.xbtn_NewGUID.TabIndex = 1;
			this.xbtn_NewGUID.TabStop = false;
			this.xbtn_NewGUID.UseVisualStyleBackColor = true;
			this.xbtn_NewGUID.Click += new System.EventHandler(this.NewGUID_Click);
			// 
			// UC_Config
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.xbtn_NewGUID);
			this.Controls.Add(this.xBxS_GUID);
			this.Name = "UC_Config";
			this.Size = new System.Drawing.Size(278, 37);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.TextBox xBxS_GUID;
		private System.Windows.Forms.Button xbtn_NewGUID;
		}
	}
