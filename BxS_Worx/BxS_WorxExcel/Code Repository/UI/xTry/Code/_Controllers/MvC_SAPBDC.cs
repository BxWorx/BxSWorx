using System;
//.........................................................
using BxS_WorxExcel.Code_Repository.UI.xTry;
using BxS_WorxExcel.MVVM;

using BxS_WorxIPX.BDC;
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class MvC_SAPBDC : MvCBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MvC_SAPBDC(	string					id
													,	IBDC_Controller	ipxBDCCntlr	) : base(id)
					{
						this._IPXBDCCntlr	=	ipxBDCCntlr;
						//...
						this._VM	=	new Lazy<VM_SAPBDC>(
							()=>	{
											var y					= new MD_SAPBDC( this._IPXBDCCntlr );
											var lo_VM			=	new	VM_SAPBDC( y );
											this._VMBase	= lo_VM;
											return	lo_VM;
										} );
					}

			#endregion


				private	VW_SAPBDC								XView			{	get	{	return	this._VM.Value.MyView;					}	}
				private	DTO_SAPSessionRequest		Request		{	get	{	return	this._VM.Value.MyModel.Request;	}	}



			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<VM_SAPBDC>		_VM;
				//...
				private	readonly	IBDC_Controller		_IPXBDCCntlr;

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void Shutdown()
					{
						this._VM.Value.View.FormClosed -= this.OnFormClosed;
						this._VM.Value.View.Close();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void	ToggleView()
					{
						if ( this._VM.Value.View == null )
							{
								this.LoadView();
							}
						//...
						this._VM.Value.ToggleView();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void LoadBindings()
					{
						this.BindControl(	this.XView.x  .Controls  )
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadView()
					{
						var lo_View						=	new	VW_SAPBDC();
						lo_View.FormClosed	 += this.OnFormClosed;
						this._VM.Value.MyView	=	lo_View;
						this._VM.Value.View		= lo_View;
					}

			#endregion

		}
}
