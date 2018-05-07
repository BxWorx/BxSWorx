using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxExcel.UI.UC;

namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
	{
	public partial class Form1 : Form
		{

			private readonly WSConfigVM x = new WSConfigVM();

		public Form1()
			{
				InitializeComponent();
				this.UC_WSConfig.ViewModel	= this.x;

				this.xcbx_Active.DataBindings.Add( this.CreateBinding( "Checked"	, "Active"	) );
				this.button1.DataBindings.Add( this.CreateBinding( "Text"	, "DisMode"	) );
			}

		private void button1_Click(object sender , EventArgs e)
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
