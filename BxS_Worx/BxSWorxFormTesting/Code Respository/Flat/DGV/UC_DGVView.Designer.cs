namespace BxS_WorxExcel.UI.Forms
	{
	partial class UC_DGVView
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.xdgv_DGV = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.xdgv_DGV)).BeginInit();
			this.SuspendLayout();
			// 
			// xdgv_DGV
			// 
			this.xdgv_DGV.AllowUserToAddRows = false;
			this.xdgv_DGV.AllowUserToDeleteRows = false;
			this.xdgv_DGV.AllowUserToOrderColumns = true;
			this.xdgv_DGV.AllowUserToResizeRows = false;
			this.xdgv_DGV.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.xdgv_DGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.xdgv_DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.xdgv_DGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.MediumSpringGreen;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.xdgv_DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.xdgv_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.xdgv_DGV.DefaultCellStyle = dataGridViewCellStyle2;
			this.xdgv_DGV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xdgv_DGV.Location = new System.Drawing.Point(1, 1);
			this.xdgv_DGV.Name = "xdgv_DGV";
			this.xdgv_DGV.ReadOnly = true;
			this.xdgv_DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.xdgv_DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.xdgv_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.xdgv_DGV.ShowCellErrors = false;
			this.xdgv_DGV.ShowCellToolTips = false;
			this.xdgv_DGV.ShowEditingIcon = false;
			this.xdgv_DGV.ShowRowErrors = false;
			this.xdgv_DGV.Size = new System.Drawing.Size(388, 242);
			this.xdgv_DGV.TabIndex = 0;
			this.xdgv_DGV.TabStop = false;
			// 
			// UC_DGVView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.Controls.Add(this.xdgv_DGV);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "UC_DGVView";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(390, 244);
			this.Load += new System.EventHandler(this.UC_DGVView_Load);
			((System.ComponentModel.ISupportInitialize)(this.xdgv_DGV)).EndInit();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.DataGridView xdgv_DGV;
		}
	}
