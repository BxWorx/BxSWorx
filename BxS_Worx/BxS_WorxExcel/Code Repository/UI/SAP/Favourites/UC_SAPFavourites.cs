using System;
using System.ComponentModel;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Code_Repository.UI.SAP
{
	internal partial class UC_SAPFavourites : UserControl
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_SAPFavourites( BindingList<DTO_FLNode>	list )
					{
						InitializeComponent();
						//...
						this._BL	= list;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly BindingList<DTO_FLNode>	_BL;
				//...
				private	BindingSource		_BS;

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ev_UCLoad( object sender , EventArgs e )
					{
						this.ConfigureDGV();
						this.configDGVColumns();
						this.ConfigureDGVBindings();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureDGVBindings()
					{
						this._BS = new BindingSource	{	DataSource = this._BL	};
						this.BxSDGV.DataSource	=	_BS;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureDGV()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void configDGVColumns()
					{
						var b	= new DataGridViewButtonColumn	{
																											Name	= "Sel"
																										, HeaderText	= string.Empty
																										,	UseColumnTextForButtonValue	= true
																										, Text	= "Click"
																										, FlatStyle	= FlatStyle.System
																									};

						var x = new DataGridViewTextBoxColumn	{
																											Name							= "SAPID"
																										,	HeaderText				= "SAP System"
																										,	DataPropertyName	= "SAPID"
																									};
						//...
						this.BxSDGV.Columns.Add(b);
						this.BxSDGV.Columns.Add(x);
					}

			#endregion

		}
}
