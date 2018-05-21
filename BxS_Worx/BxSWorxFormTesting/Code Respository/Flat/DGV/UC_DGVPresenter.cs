using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
						this.Model	=	model	;
						this.View		= view	;
						//...
						this._BS	=	new	Lazy<BindingSource>							( ()=>	new	BindingSource()							);
						this._BL	=	new	Lazy<BindingList<IDTO_Session>>	(	()=>	new	BindingList<IDTO_Session>()	);
						//...
						this.DGView.DataSource	= this._BL.Value;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<BindingSource>							_BS;
				private	readonly	Lazy<BindingList<IDTO_Session>>	_BL;

			#endregion

			//===========================================================================================
			#region "Properties"

				private		IUC_DGVModel	Model		{ get; }
				private		DataGridView	DGView	{ get =>	this.View.DGView;	}
				//...
				internal	IUC_DGVView		View		{ get; }
				internal	Color					Colour	{ set	=>	this.View.HeaderStyle.BackColor = value	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	IList<IDTO_Session>	GetSelected()
					{
						IList<IDTO_Session>	lt	=	new	List<IDTO_Session>();
						//...
						foreach ( object item in this.View.DGView.SelectedRows )
							{
							}
						//...
						return	lt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	void LoadData( IDTO_SessionRequest forRequest )
					{
						this._BL.Value.Clear();
						//...
						foreach ( IDTO_Session lo_Item in this.Model.FetchData( forRequest ) )
							{
								this._BL.Value.Add( lo_Item );
							}
					}

			#endregion

		}
}
