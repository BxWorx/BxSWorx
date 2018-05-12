using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI
{
	internal class SAPBDCViewModel
		{
			internal	SAPBDCViewModel()
				{
				}

			private 	IView 	_View;

			internal	event Action Closing;


			internal IView	View	{ set { this._View	= value;
																		this._View.Closing	+=	this.OnClosing; } }

			internal	void	Shutdown()		=>	this._View?.Shutdown()		;
			internal	void	ToggleView()	=>	this._View?.ToggleView()	;

			private void OnClosing()	=>	Closing();
		}
}
