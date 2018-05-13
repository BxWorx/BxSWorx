using System;
//.........................................................
using BxS_WorxExcel.Code_Repository.UI.xTry;
using BxS_WorxExcel.MVVM;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class MvC_SAPBDC : MvCBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MvC_SAPBDC( string	id ) : base(id)
					{
						this._VM	=	new Lazy<VM_SAPBDC>( ()=> {	var lo_VM			=	new	VM_SAPBDC();
																										this._VMBase	= lo_VM;
																										return	lo_VM;									} );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy<VM_SAPBDC> _VM;

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
								var lo_View					=	new	SAPBDCView();
								lo_View.FormClosed += this.OnFormClosed;
								this._VM.Value.View	= lo_View;
							}
						//...
						this._VM.Value.ToggleView();
					}

			#endregion

		}
}
