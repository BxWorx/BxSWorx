using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxExcel.UI;

namespace BxS_WorxExcel.Code_Repository.UI.xTry
{
		internal class SAPBDCViewModel
			{
				internal	SAPBDCViewModel( Form view )
					{
						this._View	= view;

						this._View.FormClosing += this.SAPBDCView_FormClosing;

					}

				private readonly	Form 	_View;


				private void SAPBDCView_FormClosing(object sender , FormClosingEventArgs e)
					{
					}

			}
}
