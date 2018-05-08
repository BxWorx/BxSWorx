using System;
using System.Windows.Forms;
using BxS_WorxExcel.UI.UC;

namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
	{
	public partial class Form1 : Form
		{
			private readonly WSConfigVM x;

		public Form1()
			{
				InitializeComponent();

				this.x = new WSConfigVM	{		XLHndlr = Globals.ThisAddIn._XLHndlr.Value
																	,	GUID = Guid.NewGuid()												};

				var uc = new UC_WSConfigVW( this.x )	{		Name			= "UC_WSConfig"
																								,	Dock			= DockStyle.Fill
																								,	ViewModel	= this.x					};

				this.splitContainer1.Panel2.Controls.Add(uc);

				this.xcbx_Active.DataBindings.Add( this.CreateBinding( "Checked"	, "Active"	) );
				this.button1.DataBindings.Add( this.CreateBinding( "Text"	, "DisMode"	) );
			}

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
