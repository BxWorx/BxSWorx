using System;
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
						//.............................................
						this.ViewModel	= new	WSConfigVM();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const	string	PropNme_SelVal	= "SelectedValue";
				private	const	string	PropNme_Text		= "Text";
				private	const	string	PropNme_Checkd	= "Checked";

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
						this.LoadIDPage()	;
						this.LoadSAPPage();
						this.LoadWSPage()	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadIDPage()
					{
						this.xtbx_GUID	.DataBindings.Add( this.ViewModel.BindGUID		( PropNme_Text		) );
						this.xcbx_Active.DataBindings.Add( this.ViewModel.BindIsActive( PropNme_Checkd	) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadWSPage()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSAPPage()
					{
						this.xtbx_SsnNme	.DataBindings.Add( this.ViewModel.BindSessionID		( PropNme_Text ) );
						this.xtbx_SAPTCde	.DataBindings.Add( this.ViewModel.BindSAPTCode		( PropNme_Text ) );
						this.xtbx_Pause		.DataBindings.Add( this.ViewModel.BindPauseTime		( PropNme_Text ) );
						this.xcbx_Skip1st	.DataBindings.Add( this.ViewModel.BindSkip1st			( PropNme_Text ) );
						//...
						this.xcbx_CTUDisp.DataSource			= this.ViewModel.CTUDspList;
						this.xcbx_CTUDisp.DisplayMember		=	this.ViewModel.DisplayMem	;
						this.xcbx_CTUDisp.ValueMember			= this.ViewModel.ValueMem		;

						this.xcbx_CTUDisp	.DataBindings.Add( this.ViewModel.BindCTUDispList( PropNme_SelVal ) );
						//.............................................
						this.xcbx_CTUUpdt.DataSource			= this.ViewModel.CTUUpdList	;
						this.xcbx_CTUUpdt.DisplayMember		=	this.ViewModel.DisplayMem	;
						this.xcbx_CTUUpdt.ValueMember			= this.ViewModel.ValueMem		;

						this.xcbx_CTUUpdt	.DataBindings.Add(	this.ViewModel.BindCTUUpdtList( PropNme_SelVal	)	);
						//.............................................
						this.xcbx_CTUDflt	.DataBindings.Add(	this.ViewModel.BindDefltSize	(	PropNme_Checkd	) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	Binding CreateBinding( string vwName , string vmName , DataSourceUpdateMode mode	= DataSourceUpdateMode.OnPropertyChanged )
					{
						return	new	Binding(	vwName , this.ViewModel	, vmName , true , mode );
					}

			#endregion

		}
}
