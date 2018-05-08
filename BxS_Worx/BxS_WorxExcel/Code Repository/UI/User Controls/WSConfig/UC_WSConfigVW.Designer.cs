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
			this.xtab_Main = new System.Windows.Forms.TabControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.xbtn_ViewPwd = new System.Windows.Forms.Button();
			this.xcbx_Protected = new System.Windows.Forms.CheckBox();
			this.xtbx_Password = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.xcbx_Active = new System.Windows.Forms.CheckBox();
			this.xtbx_GUID = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.xcbx_CTUDisp = new System.Windows.Forms.ComboBox();
			this.xtbx_SsnNme = new System.Windows.Forms.TextBox();
			this.xcbx_CTUUpdt = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.xtbx_SAPTCde = new System.Windows.Forms.TextBox();
			this.xcbx_CTUDflt = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.xtbx_Pause = new System.Windows.Forms.MaskedTextBox();
			this.xcbx_Skip1st = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.xtbx_ColMsg = new System.Windows.Forms.MaskedTextBox();
			this.xBtn_ExcelAddress = new System.Windows.Forms.Button();
			this.xtbx_DataRow = new System.Windows.Forms.MaskedTextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.xtbx_ColID = new System.Windows.Forms.MaskedTextBox();
			this.xtbx_ColExec = new System.Windows.Forms.MaskedTextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.xtbx_ColActive = new System.Windows.Forms.MaskedTextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.xtab_Main.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtab_Main
			// 
			this.xtab_Main.Controls.Add(this.tabPage3);
			this.xtab_Main.Controls.Add(this.tabPage1);
			this.xtab_Main.Controls.Add(this.tabPage4);
			this.xtab_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtab_Main.Location = new System.Drawing.Point(0, 0);
			this.xtab_Main.Name = "xtab_Main";
			this.xtab_Main.SelectedIndex = 0;
			this.xtab_Main.Size = new System.Drawing.Size(281, 210);
			this.xtab_Main.TabIndex = 2;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox6);
			this.tabPage3.Controls.Add(this.groupBox5);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(273, 184);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "ID";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// groupBox6
			// 
			this.groupBox6.BackColor = System.Drawing.Color.WhiteSmoke;
			this.groupBox6.Controls.Add(this.xbtn_ViewPwd);
			this.groupBox6.Controls.Add(this.xcbx_Protected);
			this.groupBox6.Controls.Add(this.xtbx_Password);
			this.groupBox6.Location = new System.Drawing.Point(3, 105);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(255, 72);
			this.groupBox6.TabIndex = 20;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "groupBox6";
			// 
			// xbtn_ViewPwd
			// 
			this.xbtn_ViewPwd.Location = new System.Drawing.Point(174, 42);
			this.xbtn_ViewPwd.Name = "xbtn_ViewPwd";
			this.xbtn_ViewPwd.Size = new System.Drawing.Size(26, 23);
			this.xbtn_ViewPwd.TabIndex = 7;
			this.xbtn_ViewPwd.Text = "..";
			this.xbtn_ViewPwd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.xbtn_ViewPwd.UseVisualStyleBackColor = true;
			this.xbtn_ViewPwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Xbtn_ViewPwd_MouseDown);
			this.xbtn_ViewPwd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Xbtn_ViewPwd_MouseUp);
			// 
			// xcbx_Protected
			// 
			this.xcbx_Protected.AutoSize = true;
			this.xcbx_Protected.Location = new System.Drawing.Point(6, 22);
			this.xcbx_Protected.Name = "xcbx_Protected";
			this.xcbx_Protected.Size = new System.Drawing.Size(72, 17);
			this.xcbx_Protected.TabIndex = 5;
			this.xcbx_Protected.Text = "Protected";
			this.xcbx_Protected.UseVisualStyleBackColor = true;
			this.xcbx_Protected.Click += new System.EventHandler(this.Xcbx_Protected_Click);
			// 
			// xtbx_Password
			// 
			this.xtbx_Password.Location = new System.Drawing.Point(6, 45);
			this.xtbx_Password.Name = "xtbx_Password";
			this.xtbx_Password.ReadOnly = true;
			this.xtbx_Password.Size = new System.Drawing.Size(160, 20);
			this.xtbx_Password.TabIndex = 6;
			this.xtbx_Password.UseSystemPasswordChar = true;
			this.xtbx_Password.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Xbtn_ViewPwd_MouseDown);
			this.xtbx_Password.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Xbtn_ViewPwd_MouseUp);
			// 
			// groupBox5
			// 
			this.groupBox5.BackColor = System.Drawing.Color.WhiteSmoke;
			this.groupBox5.Controls.Add(this.xcbx_Active);
			this.groupBox5.Controls.Add(this.xtbx_GUID);
			this.groupBox5.Controls.Add(this.button1);
			this.groupBox5.Location = new System.Drawing.Point(3, 3);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(255, 96);
			this.groupBox5.TabIndex = 19;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "groupBox5";
			// 
			// xcbx_Active
			// 
			this.xcbx_Active.AutoSize = true;
			this.xcbx_Active.Location = new System.Drawing.Point(19, 23);
			this.xcbx_Active.Name = "xcbx_Active";
			this.xcbx_Active.Size = new System.Drawing.Size(67, 17);
			this.xcbx_Active.TabIndex = 2;
			this.xcbx_Active.Text = "Is Active";
			this.xcbx_Active.UseVisualStyleBackColor = true;
			// 
			// xtbx_GUID
			// 
			this.xtbx_GUID.Location = new System.Drawing.Point(6, 52);
			this.xtbx_GUID.Name = "xtbx_GUID";
			this.xtbx_GUID.ReadOnly = true;
			this.xtbx_GUID.Size = new System.Drawing.Size(236, 20);
			this.xtbx_GUID.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(125, 23);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "New";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(273, 184);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "SAP Session";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.BackColor = System.Drawing.Color.WhiteSmoke;
			this.groupBox4.Controls.Add(this.xcbx_CTUDisp);
			this.groupBox4.Controls.Add(this.xtbx_SsnNme);
			this.groupBox4.Controls.Add(this.xcbx_CTUUpdt);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.xtbx_SAPTCde);
			this.groupBox4.Controls.Add(this.xcbx_CTUDflt);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.xtbx_Pause);
			this.groupBox4.Controls.Add(this.xcbx_Skip1st);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Location = new System.Drawing.Point(6, 6);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(257, 175);
			this.groupBox4.TabIndex = 15;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "groupBox4";
			// 
			// xcbx_CTUDisp
			// 
			this.xcbx_CTUDisp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.xcbx_CTUDisp.FormattingEnabled = true;
			this.xcbx_CTUDisp.Location = new System.Drawing.Point(123, 121);
			this.xcbx_CTUDisp.MaxDropDownItems = 4;
			this.xcbx_CTUDisp.Name = "xcbx_CTUDisp";
			this.xcbx_CTUDisp.Size = new System.Drawing.Size(125, 21);
			this.xcbx_CTUDisp.TabIndex = 0;
			// 
			// xtbx_SsnNme
			// 
			this.xtbx_SsnNme.Location = new System.Drawing.Point(148, 22);
			this.xtbx_SsnNme.Name = "xtbx_SsnNme";
			this.xtbx_SsnNme.Size = new System.Drawing.Size(100, 20);
			this.xtbx_SsnNme.TabIndex = 5;
			// 
			// xcbx_CTUUpdt
			// 
			this.xcbx_CTUUpdt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.xcbx_CTUUpdt.FormattingEnabled = true;
			this.xcbx_CTUUpdt.Location = new System.Drawing.Point(123, 148);
			this.xcbx_CTUUpdt.MaxDropDownItems = 4;
			this.xcbx_CTUUpdt.Name = "xcbx_CTUUpdt";
			this.xcbx_CTUUpdt.Size = new System.Drawing.Size(125, 21);
			this.xcbx_CTUUpdt.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.CausesValidation = false;
			this.label1.Location = new System.Drawing.Point(40, 129);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Display Mode.:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(54, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Pause Time";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.CausesValidation = false;
			this.label2.Location = new System.Drawing.Point(40, 156);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Update Mode.:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// xtbx_SAPTCde
			// 
			this.xtbx_SAPTCde.Location = new System.Drawing.Point(148, 48);
			this.xtbx_SAPTCde.Name = "xtbx_SAPTCde";
			this.xtbx_SAPTCde.Size = new System.Drawing.Size(100, 20);
			this.xtbx_SAPTCde.TabIndex = 8;
			// 
			// xcbx_CTUDflt
			// 
			this.xcbx_CTUDflt.AutoSize = true;
			this.xcbx_CTUDflt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.xcbx_CTUDflt.Location = new System.Drawing.Point(162, 103);
			this.xcbx_CTUDflt.Name = "xcbx_CTUDflt";
			this.xcbx_CTUDflt.Size = new System.Drawing.Size(81, 17);
			this.xcbx_CTUDflt.TabIndex = 4;
			this.xcbx_CTUDflt.Text = "Default size";
			this.xcbx_CTUDflt.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(42, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "SAP Transaction";
			// 
			// xtbx_Pause
			// 
			this.xtbx_Pause.Location = new System.Drawing.Point(148, 74);
			this.xtbx_Pause.Name = "xtbx_Pause";
			this.xtbx_Pause.Size = new System.Drawing.Size(100, 20);
			this.xtbx_Pause.TabIndex = 10;
			// 
			// xcbx_Skip1st
			// 
			this.xcbx_Skip1st.AutoSize = true;
			this.xcbx_Skip1st.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.xcbx_Skip1st.Location = new System.Drawing.Point(36, 103);
			this.xcbx_Skip1st.Name = "xcbx_Skip1st";
			this.xcbx_Skip1st.Size = new System.Drawing.Size(101, 17);
			this.xcbx_Skip1st.TabIndex = 9;
			this.xcbx_Skip1st.Text = "Skip 1st Screen";
			this.xcbx_Skip1st.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(42, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Session Name";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.groupBox2);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(273, 184);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Worksheet";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.groupBox2.Controls.Add(this.xtbx_ColMsg);
			this.groupBox2.Controls.Add(this.xBtn_ExcelAddress);
			this.groupBox2.Controls.Add(this.xtbx_DataRow);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.xtbx_ColID);
			this.groupBox2.Controls.Add(this.xtbx_ColExec);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.xtbx_ColActive);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Location = new System.Drawing.Point(3, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(233, 176);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Column References:";
			// 
			// xtbx_ColMsg
			// 
			this.xtbx_ColMsg.Location = new System.Drawing.Point(116, 145);
			this.xtbx_ColMsg.Name = "xtbx_ColMsg";
			this.xtbx_ColMsg.Size = new System.Drawing.Size(100, 20);
			this.xtbx_ColMsg.TabIndex = 15;
			this.xtbx_ColMsg.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// xBtn_ExcelAddress
			// 
			this.xBtn_ExcelAddress.Image = global::BxS_WorxExcel.Properties.Resources.if_table_excel_647591;
			this.xBtn_ExcelAddress.Location = new System.Drawing.Point(185, 13);
			this.xBtn_ExcelAddress.Name = "xBtn_ExcelAddress";
			this.xBtn_ExcelAddress.Size = new System.Drawing.Size(42, 42);
			this.xBtn_ExcelAddress.TabIndex = 15;
			this.xBtn_ExcelAddress.TabStop = false;
			this.xBtn_ExcelAddress.UseVisualStyleBackColor = true;
			this.xBtn_ExcelAddress.Click += new System.EventHandler(this.GetExcelAddress_Click);
			// 
			// xtbx_DataRow
			// 
			this.xtbx_DataRow.Location = new System.Drawing.Point(116, 28);
			this.xtbx_DataRow.Name = "xtbx_DataRow";
			this.xtbx_DataRow.Size = new System.Drawing.Size(33, 20);
			this.xtbx_DataRow.TabIndex = 12;
			this.xtbx_DataRow.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 28);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Data TopLeft";
			// 
			// xtbx_ColID
			// 
			this.xtbx_ColID.Location = new System.Drawing.Point(116, 67);
			this.xtbx_ColID.Name = "xtbx_ColID";
			this.xtbx_ColID.Size = new System.Drawing.Size(100, 20);
			this.xtbx_ColID.TabIndex = 15;
			this.xtbx_ColID.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// xtbx_ColExec
			// 
			this.xtbx_ColExec.Location = new System.Drawing.Point(116, 119);
			this.xtbx_ColExec.Name = "xtbx_ColExec";
			this.xtbx_ColExec.Size = new System.Drawing.Size(100, 20);
			this.xtbx_ColExec.TabIndex = 15;
			this.xtbx_ColExec.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(23, 70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(67, 13);
			this.label8.TabIndex = 14;
			this.label8.Text = "Identification";
			// 
			// xtbx_ColActive
			// 
			this.xtbx_ColActive.Location = new System.Drawing.Point(116, 92);
			this.xtbx_ColActive.Name = "xtbx_ColActive";
			this.xtbx_ColActive.Size = new System.Drawing.Size(100, 20);
			this.xtbx_ColActive.TabIndex = 15;
			this.xtbx_ColActive.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(23, 95);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(37, 13);
			this.label9.TabIndex = 14;
			this.label9.Text = "Active";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(19, 119);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 13);
			this.label10.TabIndex = 14;
			this.label10.Text = "Execute";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(23, 143);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(55, 13);
			this.label11.TabIndex = 14;
			this.label11.Text = "Messages";
			// 
			// UC_WSConfigVW
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.xtab_Main);
			this.DoubleBuffered = true;
			this.Name = "UC_WSConfigVW";
			this.Size = new System.Drawing.Size(281, 210);
			this.xtab_Main.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion
		private System.Windows.Forms.TabControl xtab_Main;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ComboBox xcbx_CTUDisp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox xcbx_CTUUpdt;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox xcbx_CTUDflt;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.CheckBox xcbx_Active;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox xtbx_SAPTCde;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox xtbx_SsnNme;
		private System.Windows.Forms.CheckBox xcbx_Skip1st;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.MaskedTextBox xtbx_Pause;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.MaskedTextBox xtbx_DataRow;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.MaskedTextBox xtbx_ColMsg;
		private System.Windows.Forms.MaskedTextBox xtbx_ColExec;
		private System.Windows.Forms.MaskedTextBox xtbx_ColActive;
		private System.Windows.Forms.MaskedTextBox xtbx_ColID;
		private System.Windows.Forms.TextBox xtbx_GUID;
		private System.Windows.Forms.Button xBtn_ExcelAddress;
		private System.Windows.Forms.CheckBox xcbx_Protected;
		private System.Windows.Forms.TextBox xtbx_Password;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button xbtn_ViewPwd;
		}
	}
