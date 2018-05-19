namespace BxS_WorxExcel.UI.Forms
	{
	partial class UC_DGV
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
			this.xdgv_DGV = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.xdgv_DGV)).BeginInit();
			this.SuspendLayout();
			// 
			// xdgv_DGV
			// 
			this.xdgv_DGV.AllowUserToAddRows = false;
			this.xdgv_DGV.AllowUserToDeleteRows = false;
			this.xdgv_DGV.AllowUserToOrderColumns = true;
			this.xdgv_DGV.BackgroundColor = System.Drawing.Color.LightGray;
			this.xdgv_DGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.xdgv_DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.xdgv_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.xdgv_DGV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xdgv_DGV.Location = new System.Drawing.Point(0, 0);
			this.xdgv_DGV.Name = "xdgv_DGV";
			this.xdgv_DGV.ReadOnly = true;
			this.xdgv_DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.xdgv_DGV.Size = new System.Drawing.Size(597, 442);
			this.xdgv_DGV.TabIndex = 0;
			// 
			// UC_DGV
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.Controls.Add(this.xdgv_DGV);
			this.Name = "UC_DGV";
			this.Size = new System.Drawing.Size(597, 442);
			this.Load += new System.EventHandler(this.UC_DGV_Load);
			((System.ComponentModel.ISupportInitialize)(this.xdgv_DGV)).EndInit();
			this.ResumeLayout(false);

			}

		#endregion

		internal System.Windows.Forms.DataGridView xdgv_DGV;
		}
	}
