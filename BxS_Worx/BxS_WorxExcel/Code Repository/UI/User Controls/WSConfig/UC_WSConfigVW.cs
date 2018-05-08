using System;
using System.Windows.Forms;
using BxS_WorxExcel.UI.UC;
using BxS_WorxExcel.UI;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig
{
	public partial class UC_WSConfigVW : UserControl , IView<WSConfigVM>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_WSConfigVW()
					{
						InitializeComponent();
						//.............................................
						this.ViewModel	= new	WSConfigVM();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_WSConfigVW(WSConfigVM	vm)
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
				private	const	string	PNME_SELVAL		= "SelectedValue";
				private	const	string	PNME_TEXT			= "Text";
				private	const	string	PNME_CHECK		= "Checked";
				//.................................................
				private	Control	_LastControl;

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
						this.PrepControls()	;
						this.BindIDPage()		;
						this.BindSAPPage()	;
						this.BindWSPage()		;
					}

			#endregion

			//===========================================================================================
			#region "Events"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnControlEnter( object sender , EventArgs e )
					{
						this._LastControl	= (Control) sender;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void GetExcelAddress_Click(	object sender , EventArgs e )
					{
						if ( this._LastControl == null )		return;
						//...
						this._LastControl.Text	= this.ViewModel.GetExcelAddress();
						this._LastControl.Select();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PrepControls()
					{
						this.xcbx_CTUDisp.DataSource			= this.ViewModel.CTUDspList	;
						this.xcbx_CTUDisp.DisplayMember		=	this.ViewModel.DisplayMem	;
						this.xcbx_CTUDisp.ValueMember			= this.ViewModel.ValueMem		;
						//...
						this.xcbx_CTUUpdt.DataSource			= this.ViewModel.CTUUpdList	;
						this.xcbx_CTUUpdt.DisplayMember		=	this.ViewModel.DisplayMem	;
						this.xcbx_CTUUpdt.ValueMember			= this.ViewModel.ValueMem		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindIDPage()
					{
						this.BindControl( this.xtbx_GUID			, PNME_TEXT		, nameof( this.ViewModel.GUID				) );
						this.BindControl( this.xcbx_Active		, PNME_CHECK	, nameof( this.ViewModel.Active			) );
						this.BindControl( this.xcbx_Protected	, PNME_CHECK	, nameof( this.ViewModel.Protected	) );
						this.BindControl( this.xtbx_Password	, PNME_TEXT		, nameof( this.ViewModel.Password		) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindWSPage()
					{
						this.BindControl( this.xtbx_ColID			, PNME_TEXT , nameof( this.ViewModel.Col_ID			) );
						this.BindControl( this.xtbx_ColActive	, PNME_TEXT , nameof( this.ViewModel.Col_Active	) );
						this.BindControl( this.xtbx_ColExec		, PNME_TEXT , nameof( this.ViewModel.Col_Exec		) );
						this.BindControl( this.xtbx_ColMsg		, PNME_TEXT , nameof( this.ViewModel.Col_Msg		) );
						this.BindControl( this.xtbx_DataRow		, PNME_TEXT , nameof( this.ViewModel.Data				) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindSAPPage()
					{
						this.BindControl( this.xtbx_SsnNme	, PNME_TEXT		, nameof( this.ViewModel.SessionID	) );
						this.BindControl( this.xtbx_SAPTCde	, PNME_TEXT		, nameof( this.ViewModel.SAPTCode		) );
						this.BindControl( this.xtbx_Pause		, PNME_TEXT		, nameof( this.ViewModel.PauseTime	) );
						this.BindControl( this.xcbx_Skip1st	, PNME_TEXT		, nameof( this.ViewModel.Skip1st		) );
						this.BindControl( this.xcbx_CTUDisp	, PNME_SELVAL	, nameof( this.ViewModel.DisMode		) );
						this.BindControl( this.xcbx_CTUUpdt	, PNME_SELVAL	, nameof( this.ViewModel.UpdMode		)	);
						this.BindControl( this.xcbx_CTUDflt	,	PNME_CHECK	, nameof( this.ViewModel.DefSize		) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BindControl( Control control , string vwName , string vmName )
					{
						control.DataBindings.Add( new	Binding(	vwName , this.ViewModel	, vmName , true , DSMODE ) );
					}

			#endregion

			//===========================================================================================
			#region "Events: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Xbtn_ViewPwd_MouseDown(object sender , MouseEventArgs e)
					{
						if ( ! this.xtbx_Password.ReadOnly )
							{
									this.xtbx_Password.UseSystemPasswordChar	= false;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Xbtn_ViewPwd_MouseUp(object sender , MouseEventArgs e)
					{
						if ( ! this.xtbx_Password.ReadOnly )
							{
								this.xtbx_Password.UseSystemPasswordChar	= true;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Xcbx_Protected_Click(object sender , EventArgs e)
					{
						this.xtbx_Password.ReadOnly	= ! ((CheckBox) sender).Checked;
					}

			#endregion

		}
}
