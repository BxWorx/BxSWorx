using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using Microsoft.Office.Tools.Ribbon;
//.........................................................
using BxS_SAPExcel.Main;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel
{
	public partial class BxSSAPtor
		{
			#region "Declarations"

				internal Handler_Excel	_HndlrExcel	;
				internal Handler_BDC		_HndlrBDC		;

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxSSAPtor_Load( object sender , RibbonUIEventArgs e )
					{
						this._HndlrExcel	= new	Handler_Excel();
						this._HndlrBDC		= new	Handler_BDC();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button1_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IExcelBDCSessionWS	lo_WS		=	this._HndlrExcel.GetWSData();
																		//IExcelBDCSessionRequest lo_WS		= this._HndlrBDC.CreateBDCSessionRequest();
																		//.....................
																		//this._HndlrExcel.GetWSActive( lo_WS );
																		//this._HndlrExcel.LoadWSCells( lo_WS );
																		this._HndlrBDC.WriteDataXML( lo_WS );
																		//.....................
																		this._HndlrExcel.WriteStatusbar( lo_WS.WSNo.ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//DTO_ExcelWorksheet X = this._HndlrExcel._ObjSerialiser.Value.DeSerialize<DTO_ExcelWorksheet>( lc_XML );
				//this._HndlrExcel._WSDTOParser.Value.Parse1Dto2D( X );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Button2_Click( object sender , RibbonControlEventArgs e )
					{
						//IList<Main.DTO_ExcelAppManifest> x = this._HndlrExcel.GetWBWSManifest();
					}

			#endregion

		}
}
