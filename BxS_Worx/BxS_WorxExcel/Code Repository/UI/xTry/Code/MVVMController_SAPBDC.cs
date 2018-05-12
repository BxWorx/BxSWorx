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

			private	IView						_VW;
			private	SAPBDCViewModel _VM;

			public void	ToggleView()
			{
				if ( this._VW == null )
					{
						var		x		= new SAPBDCView();
						this._VW	= new	View( x );
						this._VM.View	= this._VW;
					}
				this._VM.ToggleView();
			}



			public void Shutdown()
			{

			}

			public void Startup()
				{
					this._VM	= new	SAPBDCViewModel();
					this._VM.Closing	+=	this.OnClosing;
				}

			private	void	OnClosing()
				{
					this._VW	= null;
				}
		}
}
