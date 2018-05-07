using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxExcel.UI.UC;
using BxS_WorxExcel.UI;

namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
{
	public partial class UC_WSConfigVW : UserControl , IView<WSConfigVM>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_WSConfigVW()
					{
						InitializeComponent();
						this.ViewModel	= new	WSConfigVM();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public WSConfigVM	ViewModel { get; set; }

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnLoad( EventArgs e )
					{
						this.ConfigureBindings();

						this.loadCTU();

						this.xcbx_CTUDisp.DataSource	= this._CTUDisp;
						this.xcbx_CTUDisp.DisplayMember	= "Desc";
						this.xcbx_CTUDisp.ValueMember		= "ID"	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void ConfigureBindings()
					{
						this.xtbx_GUID		.DataBindings.Add( this.CreateBinding( "Text"			, "GUID"		) );
						this.xcbx_Active	.DataBindings.Add( this.CreateBinding( "Checked"	, "Active"	) );
						this.xcbx_CTUDisp	.DataBindings.Add( this.CreateBinding( "SelectedItem"	, "DisMode"	)	);
						this.label1				.DataBindings.Add( this.CreateBinding( "Text"					, "DisMode"	)	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	Binding CreateBinding( string vwName , string vmName )
					{
						return	new	Binding(	vwName
																, this.ViewModel
																, vmName
																, true
																, DataSourceUpdateMode.OnPropertyChanged );
					}

		#endregion


			private List<CTU> _CTUDisp	= new List<CTU>();
			private List<CTU> _CTUUpdt	= new List<CTU>();


			private void loadCTU()
			{
				this._CTUDisp.Add( new CTU { Desc = "tEST1" , ID = "1" } );
				this._CTUDisp.Add( new CTU { Desc = "tEST2" , ID = "2" } );
				this._CTUDisp.Add( new CTU { Desc = "tEST3" , ID = "3" } );
			}

			private struct CTU
			{
				public string Desc	{get; set;}
				public string	ID		{get; set;}
			}

		private void xcbx_CTUDisp_SelectedValueChanged(object sender , EventArgs e)
			{
				var x = ViewModel.DisMode;
			}




		//private void label1_Click(object sender , EventArgs e)
		//	{

		//	}
		}
}
