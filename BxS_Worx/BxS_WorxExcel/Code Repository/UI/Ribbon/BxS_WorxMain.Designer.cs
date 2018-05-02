namespace BxS_WorxExcel
	{
	partial class BxS_WorxMain : Microsoft.Office.Tools.Ribbon.RibbonBase
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public BxS_WorxMain()
				: base(Globals.Factory.GetRibbonFactory())
			{
			InitializeComponent();
			}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
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
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.button1 = this.Factory.CreateRibbonButton();
			this.button2 = this.Factory.CreateRibbonButton();
			this.button3 = this.Factory.CreateRibbonButton();
			this.xbg_SAPLogon = this.Factory.CreateRibbonGroup();
			this.dropDown1 = this.Factory.CreateRibbonDropDown();
			this.button4 = this.Factory.CreateRibbonButton();
			this.tab1.SuspendLayout();
			this.group1.SuspendLayout();
			this.xbg_SAPLogon.SuspendLayout();
			this.SuspendLayout();
			// 
			// tab1
			// 
			this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.tab1.Groups.Add(this.group1);
			this.tab1.Groups.Add(this.xbg_SAPLogon);
			this.tab1.Label = "TabAddIns";
			this.tab1.Name = "tab1";
			// 
			// group1
			// 
			this.group1.Items.Add(this.button1);
			this.group1.Items.Add(this.button2);
			this.group1.Items.Add(this.button3);
			this.group1.Label = "group1";
			this.group1.Name = "group1";
			// 
			// button1
			// 
			this.button1.Label = "Write Active";
			this.button1.Name = "button1";
			this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Button1_Click);
			// 
			// button2
			// 
			this.button2.Label = "Write ALL";
			this.button2.Name = "button2";
			this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Button2_Click);
			// 
			// button3
			// 
			this.button3.Label = "Add Config";
			this.button3.Name = "button3";
			this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Button3_Click);
			// 
			// xbg_SAPLogon
			// 
			this.xbg_SAPLogon.Items.Add(this.button4);
			this.xbg_SAPLogon.Items.Add(this.dropDown1);
			this.xbg_SAPLogon.Label = "SAP logon";
			this.xbg_SAPLogon.Name = "xbg_SAPLogon";
			// 
			// dropDown1
			// 
			this.dropDown1.Label = "dropDown1";
			this.dropDown1.Name = "dropDown1";
			this.dropDown1.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDown1_Load);
			// 
			// button4
			// 
			this.button4.Label = "Request";
			this.button4.Name = "button4";
			// 
			// BxS_WorxMain
			// 
			this.Name = "BxS_WorxMain";
			this.RibbonType = "Microsoft.Excel.Workbook";
			this.Tabs.Add(this.tab1);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BxS_WorxMain_Load);
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.xbg_SAPLogon.ResumeLayout(false);
			this.xbg_SAPLogon.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup xbg_SAPLogon;
		internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
		}

	partial class ThisRibbonCollection
		{
		internal BxS_WorxMain BxS_WorxMain
			{
			get { return this.GetRibbon<BxS_WorxMain>(); }
			}
		}
	}
