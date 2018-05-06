namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
	{
	partial class UC_WSConfigVW
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
			this.xtbx_GUID = new System.Windows.Forms.TextBox();
			this.xcbx_Active = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// xtbx_GUID
			// 
			this.xtbx_GUID.Location = new System.Drawing.Point(15, 13);
			this.xtbx_GUID.Name = "xtbx_GUID";
			this.xtbx_GUID.ReadOnly = true;
			this.xtbx_GUID.Size = new System.Drawing.Size(236, 20);
			this.xtbx_GUID.TabIndex = 0;
			// 
			// xcbx_Active
			// 
			this.xcbx_Active.AutoSize = true;
			this.xcbx_Active.Location = new System.Drawing.Point(73, 75);
			this.xcbx_Active.Name = "xcbx_Active";
			this.xcbx_Active.Size = new System.Drawing.Size(67, 17);
			this.xcbx_Active.TabIndex = 1;
			this.xcbx_Active.Text = "Is Active";
			this.xcbx_Active.UseVisualStyleBackColor = true;
			// 
			// UC_WSConfigVW
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.xcbx_Active);
			this.Controls.Add(this.xtbx_GUID);
			this.DoubleBuffered = true;
			this.Name = "UC_WSConfigVW";
			this.Size = new System.Drawing.Size(332, 106);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.TextBox xtbx_GUID;
		private System.Windows.Forms.CheckBox xcbx_Active;
		}
	}
