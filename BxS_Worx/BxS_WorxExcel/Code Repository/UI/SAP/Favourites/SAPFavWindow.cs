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
			private	BindingList<DTO_FLNode>	list = new	BindingList<DTO_FLNode>();


		public SAPFavWindow()
			{
				InitializeComponent();

				var d = new DTO_FLNode();
				d.SAPID	= "GTD";
				list.Add(d);
				list.Add(d);
				var X = new UC_SAPFavourites( list );
				X.Dock = DockStyle.Fill;
				
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
						var x = this.list[0];
						x.SAPID	= "XXX";
						this.list[0]	= x;
					}

		}
	}
