namespace BxS_WorxExcel.UI
	{
	partial class FormX
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
			this.xtbx_Test = new System.Windows.Forms.TextBox();
			this.xdgv_Main = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.xtbx_Lab = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.xdgv_Main)).BeginInit();
			this.SuspendLayout();
			// 
			// xtbx_Test
			// 
			this.xtbx_Test.Location = new System.Drawing.Point(12, 12);
			this.xtbx_Test.Name = "xtbx_Test";
			this.xtbx_Test.Size = new System.Drawing.Size(100, 20);
			this.xtbx_Test.TabIndex = 0;
			// 
			// xdgv_Main
			// 
			this.xdgv_Main.AllowUserToAddRows = false;
			this.xdgv_Main.AllowUserToDeleteRows = false;
			this.xdgv_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xdgv_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.xdgv_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.xdgv_Main.Location = new System.Drawing.Point(12, 87);
			this.xdgv_Main.Name = "xdgv_Main";
			this.xdgv_Main.ReadOnly = true;
			this.xdgv_Main.Size = new System.Drawing.Size(504, 331);
			this.xdgv_Main.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(118, 10);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 58);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// xtbx_Lab
			// 
			this.xtbx_Lab.Location = new System.Drawing.Point(213, 12);
			this.xtbx_Lab.Name = "xtbx_Lab";
			this.xtbx_Lab.ReadOnly = true;
			this.xtbx_Lab.Size = new System.Drawing.Size(100, 20);
			this.xtbx_Lab.TabIndex = 4;
			// 
			// FormX
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 430);
			this.Controls.Add(this.xtbx_Lab);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.xdgv_Main);
			this.Controls.Add(this.xtbx_Test);
			this.Name = "FormX";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FormX_Load);
			((System.ComponentModel.ISupportInitialize)(this.xdgv_Main)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion
		internal System.Windows.Forms.DataGridView xdgv_Main;
		internal System.Windows.Forms.TextBox xtbx_Test;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		internal System.Windows.Forms.TextBox xtbx_Lab;
		}
	}