using System;
using System.Windows.Forms;
using System.Drawing;
//.........................................................
using BxS_WorxExcel.UI.UC;
using BxS_WorxExcel.UI;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.SAPSessions
{
	public partial class UC_SAPSessions : UserControl , IView<SAPSessionsVM>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_SAPSessions()
					{
						InitializeComponent();
						//.............................................
						this.ViewModel	= new	SAPSessionsVM();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_SAPSessions(SAPSessionsVM	vm)
					{
						InitializeComponent();
						//.............................................
						this.ViewModel	= vm;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const DataSourceUpdateMode DSMODE	= DataSourceUpdateMode.OnPropertyChanged;
				//.................................................
				private	const	string	PNME_VAL		= "Value"	;
				private	const	string	PNME_TEXT		= "Text"	;
				//.................................................
				private	DataGridView	_DGV;
				private BindingSource	_BS;

			#endregion

			//===========================================================================================
			#region "Properties"

				public SAPSessionsVM	ViewModel { get; set; }

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnLoad( EventArgs e )
					{
						var x				= new DataGridViewCellStyle { BackColor	= Color.WhiteSmoke };
						this._DGV   = new DataGridView	{
																								Dock                            = DockStyle.Fill
																							,	Name                            = "BxSDGV"
																							, AllowUserToAddRows	            = false
																							,	AllowUserToDeleteRows	          = false
																							, AllowUserToResizeRows						= false
																							,	AlternatingRowsDefaultCellStyle	= x
																							, AutoGenerateColumns							= false
																							, MultiSelect											= true
																						};
						//...
						this.splitContainer1.Panel2.Controls.Add( this._DGV );

						this.ConfigureBindings()	;
						this.ConfigureSetup()			;
						this.ConfigureColumns()		;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindSelection()
					{
						this.BindControl( this.xtbx_User	, PNME_TEXT	, nameof( this.ViewModel.UserID				) );
						this.BindControl( this.xtbx_SsnID	, PNME_TEXT	, nameof( this.ViewModel.SessionName	) );
						this.BindControl( this.xdtp_Start	, PNME_VAL	, nameof( this.ViewModel.StartDate		) );
						this.BindControl( this.xdtp_End		, PNME_VAL	, nameof( this.ViewModel.EndDate			) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindControl( Control control , string vwName , string vmName )
					{
						control.DataBindings.Add( new	Binding( vwName , this.ViewModel	, vmName , true , DSMODE ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureBindings()
					{
						this._BS							= new BindingSource	{	DataSource = this.ViewModel.List };
						this._DGV.DataSource	=	this._BS;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureSetup()
					{
						this._DGV.AllowUserToAddRows	= false;
						this._DGV.MultiSelect         = false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ConfigureColumns()
					{
						const	string	SAPID		= "SAPID";

						var lo_C1 = new DataGridViewTextBoxColumn	{
																													Name							= SAPID
																												,	HeaderText				= "SAP System"
																												,	DataPropertyName	= this.ViewModel.PName_User
																											};
						//...
						const	string	NAME	= "NAME";

						var lo_C2 = new DataGridViewTextBoxColumn	{
																													Name							= NAME
																												,	HeaderText				= "Name"
																												,	DataPropertyName	= this.ViewModel.PName_Session
																											};
						//...
						const	string	CLIENT	= "CLIENT";

						var lo_C3 = new DataGridViewTextBoxColumn	{
																													Name							= CLIENT
																												,	HeaderText				= "Client"
																												,	DataPropertyName	= this.ViewModel.PName_SAPTCde
																											};

						var lo_C4 = new DataGridViewTextBoxColumn	{
																													Name							= "Date"
																												,	HeaderText				= "Date"
																												,	DataPropertyName	= this.ViewModel.PName_Date
																											};
						//...
						this._DGV.Columns.Add(lo_C1);
						this._DGV.Columns.Add(lo_C2);
						this._DGV.Columns.Add(lo_C3);
						this._DGV.Columns.Add(lo_C4);

						this._DGV.Columns["Date"].DefaultCellStyle.Format = " yyyy/MM/dd";
					}

			#endregion

		}
}
