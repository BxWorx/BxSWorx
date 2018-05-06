namespace BxS_WorxExcel.Code_Repository.UI.SAP
	{
	partial class UC_SAPFavourites
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_SAPFavourites));
			this.BxSDGV = new System.Windows.Forms.DataGridView();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tsl_Main = new System.Windows.Forms.ToolStrip();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.BxSDGV)).BeginInit();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tsl_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// BxSDGV
			// 
			this.BxSDGV.AllowUserToAddRows = false;
			this.BxSDGV.AllowUserToDeleteRows = false;
			this.BxSDGV.AllowUserToOrderColumns = true;
			this.BxSDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.BxSDGV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BxSDGV.Location = new System.Drawing.Point(0, 0);
			this.BxSDGV.MultiSelect = false;
			this.BxSDGV.Name = "BxSDGV";
			this.BxSDGV.Size = new System.Drawing.Size(664, 227);
			this.BxSDGV.TabIndex = 1;
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.BxSDGV);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(664, 227);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer1.LeftToolStripPanel
			// 
			this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.tsl_Main);
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(688, 227);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanelVisible = false;
			// 
			// tsl_Main
			// 
			this.tsl_Main.Dock = System.Windows.Forms.DockStyle.None;
			this.tsl_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsl_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton});
			this.tsl_Main.Location = new System.Drawing.Point(0, 3);
			this.tsl_Main.Name = "tsl_Main";
			this.tsl_Main.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsl_Main.Size = new System.Drawing.Size(24, 100);
			this.tsl_Main.TabIndex = 0;
			this.tsl_Main.Text = "UCToolstrip";
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(22, 20);
			this.openToolStripButton.Text = "&Open";
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(22, 20);
			this.saveToolStripButton.Text = "&Save";
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(22, 6);
			// 
			// cutToolStripButton
			// 
			this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
			this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripButton.Name = "cutToolStripButton";
			this.cutToolStripButton.Size = new System.Drawing.Size(22, 20);
			this.cutToolStripButton.Text = "C&ut";
			this.cutToolStripButton.Click += new System.EventHandler(this.Cut_Click);
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.Size = new System.Drawing.Size(22, 20);
			this.copyToolStripButton.Text = "&Copy";
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(BxS_WorxExcel.Main.BxS_Controller);
			// 
			// UC_SAPFavourites
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "UC_SAPFavourites";
			this.Size = new System.Drawing.Size(688, 227);
			this.Load += new System.EventHandler(this.UC_Load);
			((System.ComponentModel.ISupportInitialize)(this.BxSDGV)).EndInit();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tsl_Main.ResumeLayout(false);
			this.tsl_Main.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);

			}

		#endregion
		private System.Windows.Forms.DataGridView BxSDGV;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip tsl_Main;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton cutToolStripButton;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.BindingSource bindingSource1;
		}
	}
