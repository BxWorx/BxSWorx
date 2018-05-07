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
			this.xtab_Main = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.xcbx_CTUDisp = new System.Windows.Forms.ComboBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.xtab_Main.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtbx_GUID
			// 
			this.xtbx_GUID.Location = new System.Drawing.Point(42, 6);
			this.xtbx_GUID.Name = "xtbx_GUID";
			this.xtbx_GUID.ReadOnly = true;
			this.xtbx_GUID.Size = new System.Drawing.Size(236, 20);
			this.xtbx_GUID.TabIndex = 0;
			// 
			// xcbx_Active
			// 
			this.xcbx_Active.AutoSize = true;
			this.xcbx_Active.Location = new System.Drawing.Point(88, 46);
			this.xcbx_Active.Name = "xcbx_Active";
			this.xcbx_Active.Size = new System.Drawing.Size(67, 17);
			this.xcbx_Active.TabIndex = 1;
			this.xcbx_Active.Text = "Is Active";
			this.xcbx_Active.UseVisualStyleBackColor = true;
			// 
			// xtab_Main
			// 
			this.xtab_Main.Controls.Add(this.tabPage1);
			this.xtab_Main.Controls.Add(this.tabPage2);
			this.xtab_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtab_Main.Location = new System.Drawing.Point(0, 0);
			this.xtab_Main.Name = "xtab_Main";
			this.xtab_Main.SelectedIndex = 0;
			this.xtab_Main.Size = new System.Drawing.Size(557, 277);
			this.xtab_Main.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.comboBox1);
			this.tabPage1.Controls.Add(this.xcbx_CTUDisp);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(549, 251);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "BDC Config";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoEllipsis = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Display Mode";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(115, 33);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// xcbx_CTUDisp
			// 
			this.xcbx_CTUDisp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.xcbx_CTUDisp.FormattingEnabled = true;
			this.xcbx_CTUDisp.Location = new System.Drawing.Point(115, 6);
			this.xcbx_CTUDisp.Name = "xcbx_CTUDisp";
			this.xcbx_CTUDisp.Size = new System.Drawing.Size(121, 21);
			this.xcbx_CTUDisp.TabIndex = 0;
			this.xcbx_CTUDisp.SelectedValueChanged += new System.EventHandler(this.xcbx_CTUDisp_SelectedValueChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.xcbx_Active);
			this.tabPage2.Controls.Add(this.xtbx_GUID);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(549, 251);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoEllipsis = true;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Update Mode";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UC_WSConfigVW
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.xtab_Main);
			this.DoubleBuffered = true;
			this.Name = "UC_WSConfigVW";
			this.Size = new System.Drawing.Size(557, 277);
			this.xtab_Main.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.TextBox xtbx_GUID;
		private System.Windows.Forms.CheckBox xcbx_Active;
		private System.Windows.Forms.TabControl xtab_Main;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ComboBox xcbx_CTUDisp;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		}
	}
