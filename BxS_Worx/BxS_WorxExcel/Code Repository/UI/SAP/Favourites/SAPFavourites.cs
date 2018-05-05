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
using BxS_WorxExcel.DTO;

namespace BxS_WorxExcel.Code_Repository.UI.SAP
	{
		public partial class SAPFavourites : UserControl
			{

				private readonly BindingSource						_bsClients;
				private readonly BindingList<DTO_FLNode>	_BLFavs;
				private readonly	Button	_btnDel;

				public SAPFavourites()
					{
						InitializeComponent();

						this._btnDel	= new	Button();



						this._BLFavs	= new	BindingList<DTO_FLNode>();
						this._bsClients	= new	BindingSource();
						this.LoadClient();
						this.configDGV();
						//this.BxSDGV.DataSource	=	this._BLFavs;
						this.BxSDGV.DataSource	=	_bsClients;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadClient()
					{
						var x = new DTO_FLNode	{		SAPID		= "SAP System 01"
																			,	Client	= "300"
																		};
						this._bsClients.Add(x);
						//this._BLFavs.Add(x);
					}

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void configDGV()
					{

						var b	= new DataGridViewButtonColumn	{
																											Name	= "Sel"
																										, HeaderText	= string.Empty
																										,	UseColumnTextForButtonValue	= true
																										, Text	= "Click"
																										, FlatStyle	= FlatStyle.System
																									};

						var x = new DataGridViewTextBoxColumn	{
																											Name							= "SAPID"
																										,	HeaderText				= "SAP System"
																										,	DataPropertyName	= "SAPID"
																									};
						//...
						this.BxSDGV.Columns.Add(b);
						this.BxSDGV.Columns.Add(x);
					}

			#endregion

		}
	}
