using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxIPX.NCO;

namespace BxS_WorxExcel.UI.Forms
{
	public partial class BxS_Main : Form
		{
				private	int	_SlidePnlWidth;
				private	int	_Increment;
				private	int	_Step;

		public BxS_Main()
			{
				InitializeComponent();
				this._SlidePnlWidth	= 230;
				this._Step	= 50;
			}

		private void Xbtn_Menu_Click(object sender , EventArgs e)
			{
				this.xbtn_Menu.Enabled	= false;
				this._Increment					=	this.xpnl_SlidePanel.Width.Equals(0) ? this._Step : this._Step * -1 ;
				this.xtmr_SlidePanel.Start();
			}

		private void Xtmr_SlidePanel_Tick(object sender , EventArgs e)
			{
				this.xpnl_SlidePanel.Width	+= this._Increment;
				if (		this.xpnl_SlidePanel.Width	>= this._SlidePnlWidth
						||	this.xpnl_SlidePanel.Width	<= 0 )
					{
						this.xtmr_SlidePanel.Stop();
						this.xbtn_Menu.Enabled	= true;
					}
			}

		private	BindingList<IDTO_Session>		BDCList		{ get; set; }
		private Lazy<UC_DGV>	_DGV;

		private void button1_Click(object sender , EventArgs e)
			{
				if (this._DGV.Value.InUse)
					{	this.xpnl_UC.Controls.Remove( this._DGV.Value )	;	}
				else
					{	this.xpnl_UC.Controls.Add( this._DGV.Value )		;	}

				this._DGV.Value.InUse	= ! this._DGV.Value.InUse;
			}

		private void BxS_Main_Load(object sender , EventArgs e)
			{
				this._DGV	= new	Lazy<UC_DGV>(	()=> new UC_DGV() );
				this.BDCList	= new	BindingList<IDTO_Session>();
				this._DGV.Value.LoadData( this.BDCList );

				var x = new DTO_Session();
				x.UserID	= "AAAA";
				this.BDCList.Add(x);
			}
		//.
		}
}
