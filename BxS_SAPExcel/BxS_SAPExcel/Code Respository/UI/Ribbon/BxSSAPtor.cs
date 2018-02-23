using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Office.Tools.Ribbon;
//.........................................................
using BxS_SAPExcel.Main;
using BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel
{
	public partial class BxSSAPtor
		{
			#region "Declarations"

				internal ExcelHandler_BDCSession		_ExcelHndlr;

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxSSAPtor_Load( object sender , RibbonUIEventArgs e )
					{
						this._ExcelHndlr	= new	ExcelHandler_BDCSession();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button1_Click( object sender , RibbonControlEventArgs e )
					{
						int x =	await Task.Run( () => {
																		DTO_ExcelWorksheet lo_WS = this._ExcelHndlr.CreateBDCSessionRequest();
																		this._ExcelHndlr.LoadWSActive( lo_WS );
																		this._ExcelHndlr._WSDTOParser.Value.Parse2Dto1D( lo_WS );
																		string lc_XML	=	this._ExcelHndlr._ObjSerialiser.Value.Serialize( lo_WS );
																		DTO_ExcelWorksheet X = this._ExcelHndlr._ObjSerialiser.Value.DeSerialize<DTO_ExcelWorksheet>( lc_XML );
																		this._ExcelHndlr._WSDTOParser.Value.Parse1Dto2D( X );
																		this._ExcelHndlr._IO.Value.WriteFile($@"C:\Temp\BxSWorx\{lo_WS.WSID}.xml",lc_XML);
																		this._ExcelHndlr.WriteStatusbar( lo_WS.RowUB.ToString() );
																		Thread.Sleep(300);
																		this._ExcelHndlr.WriteStatusbar(false);
																		return	X.RowUB;
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Button2_Click( object sender , RibbonControlEventArgs e )
					{
						IList<Main.DTO_ExcelAppManifest> x = this._ExcelHndlr.WBWSManifest();
					}

			#endregion

		}
}
