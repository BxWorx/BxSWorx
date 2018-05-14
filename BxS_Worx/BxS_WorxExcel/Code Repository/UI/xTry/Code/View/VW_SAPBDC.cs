using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxExcel.MVVM;
using BxS_WorxExcel.UI;
using BxS_WorxExcel.UI.UC;

namespace BxS_WorxExcel.Code_Repository.UI.xTry
{
	public partial class VW_SAPBDC : Form , IViewForm			//<VW_SAPBDC>	 // , IViewT<SAPSessionsVM>
		{
			public VW_SAPBDC()
				{
					InitializeComponent();
					//this.ToggleView	+=	this.OnToggleView;
				}
				//public	event Action ToggleView;

				public	void OnToggleView()
					{
						if (this.Visible)
							{
								if ( this.WindowState.Equals( FormWindowState.Minimized ) )
									{	this.WindowState = FormWindowState.Normal; }
								else
									{	this.Hide(); }
							}
						else
							{	this.Show(); }
					}


		//private void SAPBDCView_Load(object sender , EventArgs e)
		//	{

		//	}
		}
}
