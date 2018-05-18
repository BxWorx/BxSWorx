using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI.Forms
	{
	public partial class UC_DGV : UserControl
		{
		public UC_DGV()
			{
				InitializeComponent();
				//...
				this.Dock	= DockStyle.Fill;
				this._BS	=	new	Lazy<BindingSource>( ()=> new	BindingSource() );
			}

		private Lazy<BindingSource>	_BS;

		public bool	InUse { get; set; }

		public void LoadData( IBindingList list)
			{
				this._BS.Value.DataSource		=	list;
			}

		private void UC_DGV_Load(object sender , EventArgs e)
			{
			}
		}
	}
