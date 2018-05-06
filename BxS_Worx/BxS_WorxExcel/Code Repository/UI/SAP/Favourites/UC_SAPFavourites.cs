using System;
using System.ComponentModel;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Code_Repository.UI.SAP
{
	public partial class UC_SAPFavourites : UserControl
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_SAPFavourites( BindingList<DTO_FLNode>	list )
					{
						InitializeComponent();
						//...
						this._BS	= new BindingSource	{	DataSource = list	};
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BindingSource	_BS;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal void LoadData( BindingList<DTO_FLNode>	list )
				//	{
				//	}

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void UC_Load( object sender , EventArgs e )
					{
						this.ConfigureBindings()	;
						this.ConfigureSetup()			;
						this.ConfigureColumns()		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Cut_Click( object sender , EventArgs e )
					{
						foreach ( object lo_Row in this.BxSDGV.SelectedRows )
							{
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureBindings()
					{
						this.BxSDGV.DataSource	=	this._BS;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureSetup()
					{
						this.BxSDGV.AllowUserToAddRows	= false;
						this.BxSDGV.MultiSelect         = false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureColumns()
					{
						const	string	SAPID		= "SAPID";

						var lo_C1 = new DataGridViewTextBoxColumn	{
																													Name							= SAPID
																												,	HeaderText				= "SAP System"
																												,	DataPropertyName	= "SAPID"
																											};
						//...
						const	string	NAME	= "NAME";

						var lo_C2 = new DataGridViewTextBoxColumn	{
																													Name							= NAME
																												,	HeaderText				= "Name"
																												,	DataPropertyName	= NAME
																											};
						//...
						const	string	CLIENT	= "CLIENT";

						var lo_C3 = new DataGridViewTextBoxColumn	{
																													Name							= CLIENT
																												,	HeaderText				= "Client"
																												,	DataPropertyName	= CLIENT
																											};
						//...
						this.BxSDGV.Columns.Add(lo_C1);
						this.BxSDGV.Columns.Add(lo_C2);
						this.BxSDGV.Columns.Add(lo_C3);
					}

			#endregion

		}
}
