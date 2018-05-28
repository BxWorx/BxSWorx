using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI.UC
	{
	[TypeDescriptionProvider(typeof(AbstractCommunicatorProvider))]
	public partial class UC_BtnSelected : UC_BtnBase
		{
		public UC_BtnSelected()	: base()
			{
			InitializeComponent();
				//...
				this.Dock	= DockStyle.Top;
			}

		private void UC_BtnSelected_Load(object sender , EventArgs e)
			{
				this.BackgroundImage	= this._Image;
			}
		}
	}
