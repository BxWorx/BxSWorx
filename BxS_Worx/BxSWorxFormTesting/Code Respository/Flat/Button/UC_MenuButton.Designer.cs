namespace BxS_WorxExcel.UI.Menu
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(( (int) ( ( (byte) ( 255 ) ) ) ) , ( (int) ( ( (byte) ( 255 ) ) ) ) , ( (int) ( ( (byte) ( 192 ) ) ) ));
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(45 , 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(3 , 45);
			this.panel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(0 , 0);
			this.button1.Margin = new System.Windows.Forms.Padding(0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(45 , 45);
			this.button1.TabIndex = 0;
			this.button1.TabStop = false;
			this.button1.UseVisualStyleBackColor = false;
			// 
			// UC_MenuButton
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_MenuButton";
			this.Size = new System.Drawing.Size(48 , 45);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		}
	}
