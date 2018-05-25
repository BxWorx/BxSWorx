using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BxSWorxFormTesting.Code_Respository.Flat.Button
{
	public partial class UC_FlipFlop : UserControl
		{
			public UC_FlipFlop()
				{
					InitializeComponent();
				}

			private void OnClick(object sender , EventArgs e)
				{
					bool	lb_Do		=	true	;
					int		ln_Inc	= 10		;
					int		ln_Pad	= this.Padding.Left		;
					int		ln_Max	=	this.Width / 2			;
					//...
					do
						{
							ln_Pad	+= ln_Inc	;
							if ( ln_Pad >= ln_Max )
								{
									lb_Do	=	false;
								}
							else
								{
									this.Padding	= new	Padding( ln_Pad , 0 , ln_Pad , 0 );
								}
						} while ( lb_Do );
				}

		//.

		}
}
