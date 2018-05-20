using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxIPX.Main;
using BxS_WorxIPX.NCO;

namespace BxS_WorxExcel.UI.Forms
{
	public partial class BxS_Main : Form
		{
				private readonly int	_SlidePnlWidth;
				private	int	_Increment;
				private readonly int	_Step;

		public BxS_Main()
			{
				InitializeComponent();
				this.xpnl_SlidePanel.BackColor	= Color.FromArgb( 150 , 1 , 1 , 1 );


				this._SlidePnlWidth	= 230;
				this._Step	= 50;

				this._IPXCntlr	=		new	Lazy<IPX_Controller>	(	()=> IPX_Controller.Instance  ) ;
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

		//private	BindingList<IDTO_Session>		BDCList		{ get; set; }
		//private Lazy<UC_DGVView>	_DGV;

		private void Button1_Click(object sender , EventArgs e)
			{
				IDTO_SessionRequest x = this._IPXCntlr.Value.NCOx_Controller.NewSAPSessionRequest();
				this._DGVP.Value.LoadData(x);
					//{	this.xpnl_UC.Controls.Remove( this._DGVP.Value.View.ViewUC );	}
					{	this.xspl_UC.Panel2Collapsed	= true;
						this.xspl_UC.Panel1.Controls.Add( this._DGVP.Value.View.ViewUC );	}

				//if (this._DGV.Value.InUse)
				//	{	this.xpnl_UC.Controls.Remove( this._DGV.Value )	;	}
				//else

				//this._DGV.Value.InUse	= ! this._DGV.Value.InUse;
			}

		private readonly Lazy<IPX_Controller>	_IPXCntlr;
		private	Lazy<UC_DGVPresenter>	_DGVP;

		private void BxS_Main_Load(object sender , EventArgs e)
			{
				IUC_DGVModel	lo_Md		= new UC_DGVModel( this._IPXCntlr.Value.NCOx_Controller	)	;
				IUC_DGVView		lo_Vw		=	new	UC_DGVView()	;
				//...
				this._DGVP	= new	Lazy<UC_DGVPresenter>(	()=>	new	UC_DGVPresenter( lo_Md , lo_Vw ) );
				//this._DGV		= new	Lazy<UC_DGVView>(	()=> new UC_DGVView() );

				//var x = new DTO_Session();
				//x.UserID	= "AAAA";
				//this.BDCList.Add(x);

				//this._DGV.Value.LoadData( this.BDCList );
			}

		private	bool	_Mousedown		;
		private	Point	_LastLocation	;

		private void panel1_MouseDown(object sender , MouseEventArgs e)
			{
				this._Mousedown	= ! this._Mousedown;
				this._LastLocation	= e.Location;
			}

		private void panel1_MouseUp(object sender , MouseEventArgs e)
			{
				this._Mousedown	= ! this._Mousedown;
			}

		private void panel1_MouseMove(object sender , MouseEventArgs e)
			{
				if ( this._Mousedown )
					{
						this.Location		= new	Point(	( this.Location.X	- this._LastLocation.X ) + e.X
																				,	(	this.Location.Y	- this._LastLocation.Y ) + e.Y );
						this.Update();
					}
			}

		private void button5_Click(object sender , EventArgs e)
			{
				IDTO_SessionRequest x = this._IPXCntlr.Value.NCOx_Controller.NewSAPSessionRequest();
				x.User	= "100";
				this._DGVP.Value.LoadData(x);
			}

		private void button6_Click(object sender , EventArgs e)
			{
				this.xdlg_Colour.ShowDialog();
				this._DGVP.Value.Colour	=	this.xdlg_Colour.Color;
			}

		//.

		}
}
