using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxExcel.DTO;

namespace BxS_WorxExcel.Code_Repository.UI.SAP.Favourites
	{
	public partial class SAPFavWindow : Form
		{
			private readonly BindingList<DTO_FLNode>	list = new	BindingList<DTO_FLNode>();

		public SAPFavWindow()
			{
				InitializeComponent();

			var d = new DTO_FLNode
				{
				SAPID = "GTD"
				};
			this.list.Add(d);
			this.list.Add(d);
			var X = new UC_SAPFavourites(this.list)
				{
				Dock = DockStyle.Fill
				};

			this.splitContainer1.Panel2.Controls.Add(X);
			}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void B1_Click( object sender , EventArgs e )
					{
						this.list.RemoveAt(0);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void B2_Click( object sender , EventArgs e )
					{
			DTO_FLNode x = this.list[0];
						x.SAPID	= "XXX";
						this.list[0]	= x;
					}
		}
	}
