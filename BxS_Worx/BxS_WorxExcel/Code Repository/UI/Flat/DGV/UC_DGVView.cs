using System.Drawing;
using System.Windows.Forms;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal partial class UC_DGVView : UserControl , IUC_DGVView
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_DGVView()
					{
						InitializeComponent();
						//...
						this.Dock		= DockStyle.Fill;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	UserControl		ViewUC	{ get	=>	this					; }
				public	DataGridView	DGView	{ get =>	this.xdgv_DGV	;	}

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void UC_DGVView_Load(object sender , System.EventArgs e)
					{
						this.SetupDataGrid();
						this.SetupColumns();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupDataGrid()
					{
						var x	= new DataGridViewCellStyle { BackColor	= Color.WhiteSmoke };
						//...
						this.DGView.Dock														= DockStyle.Fill;
						this.DGView.AllowUserToAddRows	            = false;
						this.DGView.AllowUserToDeleteRows	          = false;
						this.DGView.AllowUserToResizeRows						= false;
						this.DGView.AlternatingRowsDefaultCellStyle	= x;
						this.DGView.AutoGenerateColumns							= false;
						this.DGView.MultiSelect											= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupColumns()
					{
						this.DGView.Columns.Add(	this.SetupColumn_Text( "UserID" , "User"			, nameof( IDTO_Session.UserID )				, true										)	)	;
						this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnID"	, "Session"		, nameof( IDTO_Session.SessionName )	, false										)	)	;
						this.DGView.Columns.Add(	this.SetupColumn_Text( "SAPTCd"	, "SAP Tran"	, nameof( IDTO_Session.SAPTCode )			, false										)	)	;
						this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnDte"	, "Date"			, nameof( IDTO_Session.CreationDate	)	, false	,	"d"							)	)	;
						this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnTme"	, "Time"			, nameof( IDTO_Session.CreationTime	)	, false	,	"T"							)	)	;
						this.DGView.Columns.Add(	this.SetupColumn_Text( "SsnTrn"	, "Count"			, nameof( IDTO_Session.Count )				, false	,	"###0"	, true	)	)	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DataGridViewTextBoxColumn SetupColumn_Text(		string	columnName
																														, string	headerText
																														, string	propertyName
																														, bool		frozen				=	false
																														, string	format				= ""
																														, bool		alignRight		= false	)
					{
						var lo_DGVStyle		= new DataGridViewCellStyle	{
																															Format = format
																														, Alignment	= alignRight	?	DataGridViewContentAlignment.MiddleRight	: DataGridViewContentAlignment.MiddleLeft
																													};

						return	new DataGridViewTextBoxColumn	{
																											Name              = columnName
																										,	HeaderText        = headerText
																										,	DataPropertyName  = propertyName
																										, ReadOnly					= true
																										, Frozen						= frozen
																										, DefaultCellStyle	= lo_DGVStyle
																									};
					}

			#endregion

		}
}
