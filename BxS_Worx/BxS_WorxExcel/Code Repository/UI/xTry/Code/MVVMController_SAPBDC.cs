using System;
using BxS_WorxExcel.Code_Repository.UI.xTry;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public class MVVMController_SAPBDC : IMVVMController
		{
			public MVVMController_SAPBDC()
				{
				}

			public	event	EventHandler ShuttingDown	;
			public	event	EventHandler ShowDialogue	;

			private	IView						_VW;
			private	SAPBDCViewModel _VM;

			public void Startup()
				{
					var		x		= new SAPBDCView();
					//...
					this._VW	= new	View( x );
					this._VM	= new	SAPBDCViewModel( this._VW );
				}
		}
}
