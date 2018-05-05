using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BxS_WorxIPX.NCO;

namespace BxS_WorxExcel.Code_Repository.UI.SAP
	{
	public partial class SAPFavourites : UserControl
		{

		//private readonly BindingSource				_bsClients;
		private readonly BindingList<string>	_blClients;

		public SAPFavourites()
			{
				InitializeComponent();

				this._blClients	= new	BindingList<string>();

				this.LoadClient();
				this.xdd_Clients.DataSource	=	this._blClients;


			}


		private void LoadClient()
			{
				this._blClients.Add("100");
				this._blClients.Add("200");
			}

		}
	}
