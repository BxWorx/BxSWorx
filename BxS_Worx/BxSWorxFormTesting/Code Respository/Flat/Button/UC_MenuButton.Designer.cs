namespace BxS_WorxExcel.UI.UC
	{
	partial class UC_MenuButton
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
			this.xpnl_Selected = new System.Windows.Forms.Panel();
			this.xobj_Button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// xpnl_Selected
			// 
			this.xpnl_Selected.BackColor = System.Drawing.Color.Transparent;
			this.xpnl_Selected.Dock = System.Windows.Forms.DockStyle.Right;
			this.xpnl_Selected.Location = new System.Drawing.Point(46 , 0);
			this.xpnl_Selected.Margin = new System.Windows.Forms.Padding(0);
			this.xpnl_Selected.Name = "xpnl_Selected";
			this.xpnl_Selected.Size = new System.Drawing.Size(2 , 45);
			this.xpnl_Selected.TabIndex = 0;
			// 
			// xbtn_Button
			// 
			this.xobj_Button.BackColor = System.Drawing.Color.Transparent;
			this.xobj_Button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xobj_Button.FlatAppearance.BorderSize = 0;
			this.xobj_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xobj_Button.Location = new System.Drawing.Point(0 , 0);
			this.xobj_Button.Margin = new System.Windows.Forms.Padding(0);
			this.xobj_Button.Name = "xbtn_Button";
			this.xobj_Button.Size = new System.Drawing.Size(46 , 45);
			this.xobj_Button.TabIndex = 0;
			this.xobj_Button.TabStop = false;
			this.xobj_Button.UseVisualStyleBackColor = false;
			// 
			// UC_MenuButton
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.xobj_Button);
			this.Controls.Add(this.xpnl_Selected);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_MenuButton";
			this.Size = new System.Drawing.Size(48 , 45);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel xpnl_Selected;
		private System.Windows.Forms.Button xobj_Button;
		}
	}
