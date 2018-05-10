using System;
using System.Windows.Forms;
using BxS_WorxExcel.Code_Repository.UI.User_Controls.SAPSessions;
using BxS_WorxExcel.UI.UC;
using BxS_WorxIPX.BDC;

namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
{
	public partial class Form1 : Form
		{
			private readonly WSConfigVM			x;
			private readonly SAPSessionsVM	y;

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		public Form1( IBDC_Controller ipxbdcCntlr , bool ssn = false )
			{
				InitializeComponent();
				//...
				if (ssn )
					{
						this.y	= new	SAPSessionsVM();
						y.IPXBDCCntlr	= ipxbdcCntlr;
						var ucs	= new UC_SAPSessions( this.y ) {	Name			= "UC_SAPSessions"
																										,	Dock			= DockStyle.Fill
																										, ViewModel	= this.y						};
						//...
						this.splitContainer1.Panel2.Controls.Add(ucs);
					}
				else
					{
						this.x = new WSConfigVM	{		XLHndlr = Globals.ThisAddIn.XLHndlr
																			,	GUID		= Guid.NewGuid()						};

						var ucc = new UC_WSConfigVW( this.x )	{		Name			= "UC_WSConfig"
																										,	Dock			= DockStyle.Fill
																										,	ViewModel	= this.x					};
						//...
						this.splitContainer1.Panel2.Controls.Add(ucc);

						this.xcbx_Active.DataBindings.Add( this.CreateBinding( "Checked"	, "Active"	) );
						this.button1.DataBindings.Add( this.CreateBinding( "Text"	, "DisMode"	) );
					}
			}

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		private void Button1_Click(object sender , EventArgs e)
			{
				this.x.GUID	= Guid.NewGuid();
			}

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		private	Binding CreateBinding( string vwName , string vmName )
			{
				return	new	Binding(	vwName
														, this.x
														, vmName
														, true
														, DataSourceUpdateMode.OnPropertyChanged );
			}
		}
}
