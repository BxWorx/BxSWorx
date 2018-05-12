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
			internal	SAPBDCViewModel( IView view )
				{
					this._View	= view;

					this._View.Closing	+= this.OnClosing;
				}

			private readonly	IView 	_View;

			private void OnClosing()
				{
				}
		}
}
