namespace BxS_WorxExcel.Code_Repository.UI.SAP
	{
	partial class SAPFavourites
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
			this.components = new System.ComponentModel.Container();
			this.xdd_Clients = new System.Windows.Forms.ComboBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.bs_Clients = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_Clients)).BeginInit();
			this.SuspendLayout();
			// 
			// xdd_Clients
			// 
			this.xdd_Clients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.xdd_Clients.FormattingEnabled = true;
			this.xdd_Clients.Location = new System.Drawing.Point(298, 137);
			this.xdd_Clients.Name = "xdd_Clients";
			this.xdd_Clients.Size = new System.Drawing.Size(121, 21);
			this.xdd_Clients.TabIndex = 0;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(100, 241);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(240, 150);
			this.dataGridView1.TabIndex = 1;
			// 
			// SAPFavourites
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.xdd_Clients);
			this.Name = "SAPFavourites";
			this.Size = new System.Drawing.Size(609, 522);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_Clients)).EndInit();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ComboBox xdd_Clients;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource bs_Clients;
		}
	}
