namespace BxS_WorxExcel
	{
	partial class BxS_Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public BxS_Ribbon()
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
			Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
			this.xtab_BxS = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.btn_SaveAct = this.Factory.CreateRibbonButton();
			this.btn_SaveAll = this.Factory.CreateRibbonButton();
			this.btn_XmlCfg = this.Factory.CreateRibbonButton();
			this.xbg_SAPLogon = this.Factory.CreateRibbonGroup();
			this.xbtn_Session = this.Factory.CreateRibbonButton();
			this.button4 = this.Factory.CreateRibbonButton();
			this.btn_Submit = this.Factory.CreateRibbonButton();
			this.dropDown1 = this.Factory.CreateRibbonDropDown();
			this.button1 = this.Factory.CreateRibbonButton();
			this.group2 = this.Factory.CreateRibbonGroup();
			this.gallery1 = this.Factory.CreateRibbonGallery();
			this.button2 = this.Factory.CreateRibbonButton();
			this.comboBox1 = this.Factory.CreateRibbonComboBox();
			this.menu1 = this.Factory.CreateRibbonMenu();
			this.group4 = this.Factory.CreateRibbonGroup();
			this.xbtn_NewBDCWS = this.Factory.CreateRibbonButton();
			this.Xbtn_MVVM = this.Factory.CreateRibbonButton();
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group3 = this.Factory.CreateRibbonGroup();
			this.dropDown2 = this.Factory.CreateRibbonDropDown();
			this.xtab_BxS.SuspendLayout();
			this.group1.SuspendLayout();
			this.xbg_SAPLogon.SuspendLayout();
			this.group2.SuspendLayout();
			this.group4.SuspendLayout();
			this.tab1.SuspendLayout();
			this.group3.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtab_BxS
			// 
			this.xtab_BxS.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.xtab_BxS.Groups.Add(this.group1);
			this.xtab_BxS.Groups.Add(this.xbg_SAPLogon);
			this.xtab_BxS.Groups.Add(this.group2);
			this.xtab_BxS.Groups.Add(this.group4);
			this.xtab_BxS.Label = "BxSAP";
			this.xtab_BxS.Name = "xtab_BxS";
			// 
			// group1
			// 
			this.group1.Items.Add(this.btn_SaveAct);
			this.group1.Items.Add(this.btn_SaveAll);
			this.group1.Items.Add(this.btn_XmlCfg);
			this.group1.Label = "group1";
			this.group1.Name = "group1";
			// 
			// btn_SaveAct
			// 
			this.btn_SaveAct.Label = "Write Active";
			this.btn_SaveAct.Name = "btn_SaveAct";
			this.btn_SaveAct.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.WriteActive_Click);
			// 
			// btn_SaveAll
			// 
			this.btn_SaveAll.Label = "Write ALL";
			this.btn_SaveAll.Name = "btn_SaveAll";
			this.btn_SaveAll.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.WriteAll_Click);
			// 
			// btn_XmlCfg
			// 
			this.btn_XmlCfg.Label = "Add Config";
			this.btn_XmlCfg.Name = "btn_XmlCfg";
			this.btn_XmlCfg.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SaveXMLCfg_Click);
			// 
			// xbg_SAPLogon
			// 
			this.xbg_SAPLogon.Items.Add(this.xbtn_Session);
			this.xbg_SAPLogon.Items.Add(this.button4);
			this.xbg_SAPLogon.Items.Add(this.btn_Submit);
			this.xbg_SAPLogon.Items.Add(this.dropDown1);
			this.xbg_SAPLogon.Label = "SAP logon";
			this.xbg_SAPLogon.Name = "xbg_SAPLogon";
			// 
			// xbtn_Session
			// 
			this.xbtn_Session.Label = "Session";
			this.xbtn_Session.Name = "xbtn_Session";
			this.xbtn_Session.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Session_Click);
			// 
			// button4
			// 
			this.button4.Label = "Request";
			this.button4.Name = "button4";
			this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Request_Click);
			// 
			// btn_Submit
			// 
			this.btn_Submit.Label = "Submit";
			this.btn_Submit.Name = "btn_Submit";
			this.btn_Submit.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Submit_Click);
			// 
			// dropDown1
			// 
			this.dropDown1.Buttons.Add(this.button1);
			this.dropDown1.Label = "dropDown1";
			this.dropDown1.Name = "dropDown1";
			this.dropDown1.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDown1_Load);
			// 
			// button1
			// 
			this.button1.Label = "button1";
			this.button1.Name = "button1";
			// 
			// group2
			// 
			this.group2.Items.Add(this.gallery1);
			this.group2.Items.Add(this.comboBox1);
			this.group2.Items.Add(this.menu1);
			this.group2.Label = "group2";
			this.group2.Name = "group2";
			// 
			// gallery1
			// 
			this.gallery1.Buttons.Add(this.button2);
			this.gallery1.Label = "gallery1";
			this.gallery1.Name = "gallery1";
			// 
			// button2
			// 
			this.button2.Label = "button2";
			this.button2.Name = "button2";
			// 
			// comboBox1
			// 
			this.comboBox1.Label = "comboBox1";
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Text = null;
			// 
			// menu1
			// 
			this.menu1.Label = "menu1";
			this.menu1.Name = "menu1";
			// 
			// group4
			// 
			this.group4.Items.Add(this.xbtn_NewBDCWS);
			this.group4.Items.Add(this.Xbtn_MVVM);
			this.group4.Label = "group4";
			this.group4.Name = "group4";
			// 
			// xbtn_NewBDCWS
			// 
			this.xbtn_NewBDCWS.Label = "Create BDC";
			this.xbtn_NewBDCWS.Name = "xbtn_NewBDCWS";
			this.xbtn_NewBDCWS.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Xbtn_NewBDCWS_Click);
			// 
			// Xbtn_MVVM
			// 
			this.Xbtn_MVVM.Label = "Test MVVM";
			this.Xbtn_MVVM.Name = "Xbtn_MVVM";
			this.Xbtn_MVVM.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MVVM_Click);
			// 
			// tab1
			// 
			this.tab1.Groups.Add(this.group3);
			this.tab1.Label = "tab1";
			this.tab1.Name = "tab1";
			// 
			// group3
			// 
			this.group3.DialogLauncher = ribbonDialogLauncherImpl1;
			this.group3.Items.Add(this.dropDown2);
			this.group3.Label = "group3";
			this.group3.Name = "group3";
			// 
			// dropDown2
			// 
			this.dropDown2.Label = "dropDown2";
			this.dropDown2.Name = "dropDown2";
			this.dropDown2.ShowLabel = false;
			// 
			// BxS_Ribbon
			// 
			this.Name = "BxS_Ribbon";
			this.RibbonType = "Microsoft.Excel.Workbook";
			this.Tabs.Add(this.xtab_BxS);
			this.Tabs.Add(this.tab1);
			this.Close += new System.EventHandler(this.BxS_Ribbon_CloseAsync);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BxS_WorxMain_Load);
			this.xtab_BxS.ResumeLayout(false);
			this.xtab_BxS.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.xbg_SAPLogon.ResumeLayout(false);
			this.xbg_SAPLogon.PerformLayout();
			this.group2.ResumeLayout(false);
			this.group2.PerformLayout();
			this.group4.ResumeLayout(false);
			this.group4.PerformLayout();
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group3.ResumeLayout(false);
			this.group3.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab xtab_BxS;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_SaveAct;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_SaveAll;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_XmlCfg;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup xbg_SAPLogon;
		internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Submit;
		private Microsoft.Office.Tools.Ribbon.RibbonButton button1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
		internal Microsoft.Office.Tools.Ribbon.RibbonGallery gallery1;
		private Microsoft.Office.Tools.Ribbon.RibbonButton button2;
		internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBox1;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
		private Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
		internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton xbtn_Session;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton xbtn_NewBDCWS;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton Xbtn_MVVM;
		}

	partial class ThisRibbonCollection
		{
		internal BxS_Ribbon BxS_WorxMain
			{
			get { return this.GetRibbon<BxS_Ribbon>(); }
			}
		}
	}
