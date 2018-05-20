using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal class UC_DGVPresenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_DGVPresenter(		IUC_DGVModel	model
																	,	IUC_DGVView		view	)
					{
						this.Model		=	model	;
						this.View			= view	;
						//...
						this._BS	=	new	Lazy<BindingSource>							( ()=>	new	BindingSource()							);
						this._BL	=	new	Lazy<BindingList<IDTO_Session>>	(	()=>	new	BindingList<IDTO_Session>()	);
						//...
						this.Loaded	=	false;
						}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<BindingSource>							_BS;
				private	readonly	Lazy<BindingList<IDTO_Session>>	_BL;
				//...
				private	bool	Loaded	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private		IUC_DGVModel	Model		{ get; }
				private		DataGridView	DGView	{ get =>	this.View.DGView;	}
				//...
				internal	IUC_DGVView		View		{ get; }

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	IList<IDTO_Session>	GetSelected()
					{
						IList<IDTO_Session>	lt	=	new	List<IDTO_Session>();
						if ( this.Loaded )
							{
								foreach ( object item in this.View.DGView.SelectedRows )
									{
									}
							}
						return	lt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	void LoadData( IDTO_SessionRequest forRequest )
					{
						if ( ! this.Loaded )	{	this.SetupView();	}
						//.............................................
						this._BL.Value.Clear();

						foreach ( IDTO_Session lo_Item in this.Model.FetchData( forRequest ) )
							{
								this._BL.Value.Add( lo_Item );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupView()
					{
						this.Loaded	= ! this.Loaded	;
						//...
						this.DGView.DataSource	= this._BL.Value;
						//this.SetupDataGrid();
						//this.SetupColumns();
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void SetupDataGrid()
				//	{
				//		var x	= new DataGridViewCellStyle { BackColor	= Color.WhiteSmoke };
				//		//...
				//		this.DGView.Dock														= DockStyle.Fill;
				//		this.DGView.AllowUserToAddRows	            = false;
				//		this.DGView.AllowUserToDeleteRows	          = false;
				//		this.DGView.AllowUserToResizeRows						= false;
				//		this.DGView.AlternatingRowsDefaultCellStyle	= x;
				//		this.DGView.AutoGenerateColumns							= false;
				//		this.DGView.MultiSelect											= true;
				//		//...
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void SetupColumns()
				//	{
				//		this.DGView.Columns.Add(	this.SetupColumn_Text( "UserID" , "User"			, nameof( IDTO_Session.UserID )				, true										)	)	;
				//		this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnID"	, "Session"		, nameof( IDTO_Session.SessionName )	, false										)	)	;
				//		this.DGView.Columns.Add(	this.SetupColumn_Text( "SAPTCd"	, "SAP Tran"	, nameof( IDTO_Session.SAPTCode )			, false										)	)	;
				//		this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnDte"	, "Date"			, nameof( IDTO_Session.CreationDate	)	, false	,	"d"							)	)	;
				//		this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnTme"	, "Time"			, nameof( IDTO_Session.CreationTime	)	, false	,	"T"							)	)	;
				//		this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnTrn"	, "Count"			, nameof( IDTO_Session.Count )				, false	,	"###0"	, true	)	)	;
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private DataGridViewTextBoxColumn SetupColumn_Text(		string	columnName
				//																										, string	headerText
				//																										, string	propertyName
				//																										, bool		frozen				=	false
				//																										, string	format				= ""
				//																										, bool		alignRight		= false	)
				//	{
				//		var lo_DGVStyle		= new DataGridViewCellStyle	{
				//																											Format = format
				//																										, Alignment	= alignRight	?	DataGridViewContentAlignment.MiddleRight	: DataGridViewContentAlignment.MiddleLeft
				//																									};

				//		return	new DataGridViewTextBoxColumn	{
				//																							Name              = columnName
				//																						,	HeaderText        = headerText
				//																						,	DataPropertyName  = propertyName
				//																						, ReadOnly					= true
				//																						, Frozen						= frozen
				//																						, DefaultCellStyle	= lo_DGVStyle
				//																					};
				//	}

			#endregion

		}
}
